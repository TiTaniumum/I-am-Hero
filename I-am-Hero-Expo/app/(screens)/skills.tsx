import { Collapsible } from "@/components/Collapsible";
import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Skill } from "@/models/Skill";
import { AntDesign, Ionicons, MaterialIcons } from "@expo/vector-icons";
import { router, useFocusEffect } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import {
  ActivityIndicator,
  FlatList,
  Pressable,
  RefreshControl,
  StyleSheet,
  View,
} from "react-native";
import Animated, { FadeIn, FlipInXUp, ZoomOut } from "react-native-reanimated";

export default function SkillsScreen() {
  const { setAlpha, user, api, alert, setEditSkill } = useGlobalContext();
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const [refreshing, setRefreshing] = useState(false);
  const [loading, setLoading] = useState(true);
  const [skills, setSkills] = useState<Skill[]>([]);
  const [itemsCountdown, setItemsCountdown] = useState<
    {
      id: number;
      countdown: number;
      interval: NodeJS.Timeout | undefined;
      isDeleted: boolean;
    }[]
  >(
    skills.map((item) => {
      return {
        id: item.id,
        countdown: 0,
        interval: undefined,
        isDeleted: false,
      };
    })
  );

  const onRefresh = useCallback(() => {
    setRefreshing(true);
    setLoading(true);
    api.GetSkills().then(() => {
      setSkills(user.skills ?? []);
      setRefreshing(false);
      setLoading(false);
    });
  }, []);

  useEffect(() => {
    setItemsCountdown(
      skills.map((item) => {
        return {
          id: item.id,
          countdown: 0,
          interval: undefined,
          isDeleted: false,
        };
      })
    );
  }, [skills]);

  useFocusEffect(onRefresh);

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
          .catch((error) =>
            alert(Resource.get("error!"), Resource.get("somethingwrong"))
          );
      }
    }, 1000);
  }
  const DeletionText = useCallback(
    (id: number) => {
      const item = itemsCountdown.find((item) => item.id === id);
      return item !== undefined && item.countdown > 0
        ? `${Resource.get("cancel")} ${item.countdown}`
        : item?.isDeleted
        ? Resource.get("deleted!")
        : Resource.get("delete");
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
        style={({ pressed, hovered }) => [
          hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
          {
            position: "absolute",
            bottom: 20,
            right: 20,
            borderRadius: "100%",
            zIndex: 1000,
          },
          pressed ? { backgroundColor: color } : { transitionDuration: "0.2s" },
        ]}
        onPress={() => {
          router.push("/createbiography");
        }}
      >
        <AntDesign name="pluscircleo" size={50} color={color} />
      </Pressable>
      <Pressable
        style={({ pressed, hovered }) => [
          hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
          pressed ? { backgroundColor: color } : { transitionDuration: "0.2s" },
          {
            position: "absolute",
            bottom: 80,
            right: 15,
            borderRadius: "100%",
            zIndex: 1000,
          },
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
          data={skills}
          renderItem={({ index, item }) => (
            <Animated.View key={item.id} entering={FlipInXUp} exiting={ZoomOut}>
              <ThemedView
                key={item.id}
                style={[styles.item, { borderColor: color }]}
              >
                <Collapsible title={<ThemedView></ThemedView>}>
                  <Animated.View entering={FadeIn}>
                    <View style={styles.options}>
                      <Pressable
                        style={({ pressed, hovered }) => [
                          hovered
                            ? { backgroundColor: setAlpha(color, 0.5) }
                            : {},
                          pressed
                            ? { backgroundColor: color }
                            : { transitionDuration: "0.2s" },
                          Styles.pressable,
                          { borderColor: color },
                          styles.optionButton,
                          isDeleted(item.id) ? { borderColor: "gray" } : {},
                        ]}
                        onPress={() => {
                          if (isDeleted(item.id)) return;
                          setEditSkill(item);
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
                          {Resource.get("edit")}
                        </ThemedText>
                      </Pressable>
                      <Pressable
                        style={({ pressed, hovered }) => [
                          hovered
                            ? { backgroundColor: setAlpha(color, 0.5) }
                            : {},
                          pressed
                            ? { backgroundColor: color }
                            : { transitionDuration: "0.2s" },
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
                  {item.description}
                </ThemedText>
              </ThemedView>
            </Animated.View>
          )}
          ListEmptyComponent={
            <ThemedView style={Styles.container}>
              <ThemedText>You didn't create skills yet</ThemedText>
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
