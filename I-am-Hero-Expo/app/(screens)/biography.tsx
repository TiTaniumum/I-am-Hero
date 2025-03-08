import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import Styles from "@/constants/Styles";
import {
  ActivityIndicator,
  FlatList,
  Pressable,
  RefreshControl,
  StyleSheet,
  View,
} from "react-native";
import AntDesign from "@expo/vector-icons/AntDesign";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Colors } from "@/constants/Colors";
import { router } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import { BioPiece } from "@/models/BioPiece";
import { useGlobalContext } from "@/components/ContextProvider";
import { Collapsible } from "@/components/Collapsible";
import MaterialIcons from "@expo/vector-icons/MaterialIcons";
import Ionicons from "@expo/vector-icons/Ionicons";
import Animated, { FadeIn, FlipInXUp, ZoomOut } from "react-native-reanimated";

export default function BiographyScreen() {
  const { user, api, alert, setEditBioID, setEditBioText } = useGlobalContext();
  const [bioPieces, setBioPieces] = useState<BioPiece[]>(user.biopieces ?? []);
  const [itemsCountdown, setItemsCountdown] = useState<
    {
      id: number;
      countdown: number;
      interval: NodeJS.Timeout | undefined;
      isDeleted: boolean;
    }[]
  >(
    bioPieces.map((item) => {
      return {
        id: item.id,
        countdown: 0,
        interval: undefined,
        isDeleted: false,
      };
    })
  );
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const background = Colors[colorScheme ?? "light"].background;
  const [refreshing, setRefreshing] = useState(false);
  const [loading, setLoading] = useState(true);

  const onRefresh = useCallback(() => {
    setRefreshing(true);
    api.GetBioPieces().then(() => {
      setBioPieces(user.biopieces ?? []);
      setRefreshing(false);
      if (loading) setLoading(false);
    });
  }, []);

  useEffect(() => {
    setRefreshing(true);
    setLoading(true);
    api.GetBioPieces().then(() => {
      setBioPieces(user.biopieces ?? []);
      setRefreshing(false);
      setLoading(false);
    });
  }, []);
  useEffect(() => {
    setItemsCountdown(
      bioPieces.map((item) => {
        return {
          id: item.id,
          countdown: 0,
          interval: undefined,
          isDeleted: false,
        };
      })
    );
  }, [bioPieces]);

  function startDeletionCountdown(id: number) {
    const countdownitem = itemsCountdown.find((item) => item.id === id);
    if (countdownitem?.isDeleted) return;
    if (countdownitem !== undefined && countdownitem.countdown > 0) {
      clearInterval(countdownitem.interval);
      setItemsCountdown((prevData) =>
        prevData.map((item) =>
          item.id == id ? { ...item, countdown: 0, interval: undefined } : item
        )
      );
      return;
    }
    setItemsCountdown((prevData) =>
      prevData.map((item) =>
        item.id === id ? { ...item, countdown: 5 } : item
      )
    );
    let count = 5;
    const interval = setInterval(() => {
      count -= 1;
      setItemsCountdown((prevData) =>
        prevData.map((item) =>
          item.id === id
            ? { ...item, countdown: count, interval: interval }
            : item
        )
      );
      if (count === 0) {
        clearInterval(interval);
        setItemsCountdown((prevData) =>
          prevData.map((item) =>
            item.id === id
              ? {
                  ...item,
                  countdown: count,
                  interval: undefined,
                  isDeleted: true,
                }
              : item
          )
        );
        api
          .DeleteBioPiece(id)
          .catch((error) => alert("ERROR", "Something went wrong ..."));
      }
    }, 1000);
  }
  const DeletionText = useCallback(
    (id: number) => {
      const item = itemsCountdown.find((item) => item.id === id);
      return item !== undefined && item.countdown > 0
        ? `Cancel ${item.countdown}`
        : item?.isDeleted
        ? "Deleted!"
        : "Delete";
    },
    [itemsCountdown]
  );
  const isDeleted = useCallback(
    (id: number) => {
      const item = itemsCountdown.find((item) => item.id === id);
      return item !== undefined && item.isDeleted;
    },
    [itemsCountdown]
  );

  return (
    <ThemedView style={Styles.container}>
      <Pressable
        style={({ pressed }) => [
          {
            position: "absolute",
            bottom: 20,
            right: 20,
            borderRadius: "100%",
            zIndex: 1000,
          },
          pressed
            ? { backgroundColor: "white" }
            : { transitionDuration: "0.2s" },
        ]}
        onPress={() => {
          router.push("/createbiography");
        }}
      >
        <AntDesign name="pluscircleo" size={50} color={color} />
      </Pressable>
      <Pressable
        style={({ pressed }) => [
          {
            position: "absolute",
            bottom: 80,
            right: 15,
            borderRadius: "100%",
            zIndex: 1000,
          },
          pressed
            ? { backgroundColor: "white" }
            : { transitionDuration: "0.2s" },
        ]}
        onPress={onRefresh}
      >
        <Ionicons name="refresh-circle-outline" size={60} color={color} />
      </Pressable>
      {loading && <ActivityIndicator size="large" color={color} />}
      {!loading && (
        <FlatList
          refreshControl={
            <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
          }
          style={styles.listcontainer}
          contentContainerStyle={styles.listcontainer}
          data={bioPieces}
          renderItem={({ index, item }) => (
            <Animated.View key={item.id} entering={FlipInXUp} exiting={ZoomOut}>
              <ThemedView
                key={item.id}
                style={[styles.item, { borderColor: color }]}
              >
                <Collapsible
                  title={
                    <ThemedText style={styles.date}>
                      {item.createDate instanceof Date
                        ? item.createDate.toLocaleDateString()
                        : new Date(item.createDate).toLocaleDateString()}
                    </ThemedText>
                  }
                >
                  <Animated.View entering={FadeIn}>
                    <View style={styles.options}>
                      <Pressable
                        style={[
                          Styles.pressable,
                          { borderColor: color },
                          styles.optionButton,
                          isDeleted(item.id) ? { borderColor: "gray" } : {},
                        ]}
                        onPress={() => {
                          if (isDeleted(item.id)) return;
                          setEditBioID(item.id);
                          setEditBioText(item.text);
                          router.push("/editbiography");
                        }}
                      >
                        <MaterialIcons
                          name="mode-edit"
                          size={24}
                          color={isDeleted(item.id) ? "gray" : color}
                        />
                        <ThemedText
                          style={
                            isDeleted(item.id)
                              ? {
                                  textDecorationLine: "line-through",
                                  color: "gray",
                                }
                              : {}
                          }
                        >
                          Edit
                        </ThemedText>
                      </Pressable>
                      <Pressable
                        style={[
                          Styles.pressable,
                          { borderColor: color },
                          styles.optionButton,
                        ]}
                        onPress={() => {
                          startDeletionCountdown(item.id);
                        }}
                      >
                        <MaterialIcons name="delete" size={24} color={color} />
                        <ThemedText>{DeletionText(item.id)}</ThemedText>
                      </Pressable>
                    </View>
                  </Animated.View>
                </Collapsible>
                <ThemedText
                  style={[
                    styles.text,
                    isDeleted(item.id)
                      ? { textDecorationLine: "line-through", color: "gray" }
                      : {},
                  ]}
                >
                  {item.text}
                </ThemedText>
              </ThemedView>
            </Animated.View>
          )}
          ListEmptyComponent={
            <ThemedView style={Styles.container}>
              <ThemedText>There once was a hero</ThemedText>
              <ThemedText>named {user.hero?.name}...</ThemedText>
            </ThemedView>
          }
        />
      )}
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  listcontainer: {
    width: "100%",
  },
  item: {
    padding: 20,
    width: "100%",
    borderBottomWidth: 1,
    borderBottomColor: "white",
  },
  date: {
    color: "gray",
  },
  text: {
    paddingHorizontal: 20,
  },
  options: {
    paddingVertical: 15,
    flexDirection: "row",
    justifyContent: "space-between",
  },
  optionButton: {
    width: "40%",
    flexDirection: "row",
    gap: 10,
  },
});
