import { Collapsible } from "@/components/Collapsible";
import { useGlobalContext } from "@/components/ContextProvider";
import ProgressBar from "@/components/ProgressBar";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import { Attribute, AttributeState } from "@/models/Attribute";
import { AntDesign, Ionicons, MaterialIcons } from "@expo/vector-icons";
import { router, useFocusEffect } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import {
  ActivityIndicator,
  FlatList,
  Pressable,
  RefreshControl,
  StyleSheet,
  useColorScheme,
} from "react-native";
import Animated, { FadeIn, FlipInXUp, ZoomOut } from "react-native-reanimated";

export default function AttributesScreen() {
  const { user, api, alert, setEditAttribute, setAlpha } = useGlobalContext();
  const [attributes, setAttributes] = useState<Attribute[]>([
    ...user.attributes,
  ]);
  const [attributeStates, setAttributeStates] = useState<AttributeState[]>([
    ...user.attributeStates,
  ]);
  const [refreshing, setRefreshing] = useState(false);
  const [loading, setLoading] = useState(true);
  const [itemsCountdown, setItemsCountdown] = useState<
    {
      id: number;
      countdown: number;
      interval: NodeJS.Timeout | undefined;
      isDeleted: boolean;
    }[]
  >(
    attributes.map((item) => {
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

  const onRefresh = useCallback(() => {
    setRefreshing(true);
    setLoading(true);
    api
      .GetAttributes()
      .then(() => {
        api
          .GetAttributeStates()
          .then(() => {
            setAttributes([...user.attributes]);
            setAttributeStates([...user.attributeStates]);
            setRefreshing(false);
            setLoading(false);
          })
          .catch((error) => {
            setLoading(false);
            setRefreshing(false);
            alert(Resource.get("error!"), Resource.get("somethingwrong"));
          });
      })
      .catch((error) => {
        setLoading(false);
        setRefreshing(false);
        alert(Resource.get("error!"), Resource.get("somethingwrong"));
      });
  }, []);

  useFocusEffect(onRefresh);

  useEffect(() => {
    setItemsCountdown(
      attributes.map((item) => {
        return {
          id: item.id,
          countdown: 0,
          interval: undefined,
          isDeleted: false,
        };
      })
    );
  }, [attributes]);

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
          .DeleteAttribute(id)
          .catch((error) =>
            alert(Resource.get("error!"), Resource.get("somethingwrong"))
          );
      }
    }, 1000);
  }

  const isDeleted = useCallback(
    (id: number) => {
      const item = itemsCountdown.find((item) => item.id === id);
      return item !== undefined && item.isDeleted;
    },
    [itemsCountdown]
  );

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

  return (
    <ThemedView style={Styles.container}>
      <Pressable
        style={({ pressed, hovered }) => [
          {
            position: "absolute",
            bottom: 20,
            right: 20,
            borderRadius: "100%",
            zIndex: 1000,
          },
          hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
          pressed ? { backgroundColor: color } : { transitionDuration: "0.2s" },
        ]}
        onPress={() => {
          router.push("/createattribute");
        }}
      >
        <AntDesign name="pluscircleo" size={50} color={color} />
      </Pressable>
      <Pressable
        style={({ pressed, hovered }) => [
          {
            position: "absolute",
            bottom: 80,
            right: 15,
            borderRadius: "100%",
            zIndex: 1000,
          },
          hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
          pressed ? { backgroundColor: color } : { transitionDuration: "0.2s" },
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
          style={{ width: "100%" }}
          contentContainerStyle={{ width: "100%" }}
          data={attributes}
          renderItem={({ index, item }) => (
            <Animated.View key={item.id} entering={FlipInXUp} exiting={ZoomOut}>
              <Collapsible
                title={
                  <ThemedView
                    style={
                      item.cAttributeTypeId == 1
                        ? styles.item
                        : styles.itemStateType
                    }
                  >
                    {item.cAttributeTypeId == 1 && (
                      <>
                        <ThemedText
                          style={[
                            isDeleted(item.id)
                              ? {
                                  textDecorationLine: "line-through",
                                  color: "gray",
                                }
                              : {},
                          ]}
                        >
                          {item.name}
                        </ThemedText>
                        <ProgressBar
                          minValue={item.minValue ?? 0}
                          curValue={item.value ?? item.minValue ?? 0}
                          maxValue={item.maxValue ?? 0}
                          color={isDeleted(item.id) ? "gray" : color}
                          height={15}
                          numbersVisible
                          colorfull={!isDeleted(item.id)}
                        />
                      </>
                    )}
                    {item.cAttributeTypeId == 2 && (
                      <>
                        <ThemedText
                          style={[
                            isDeleted(item.id)
                              ? {
                                  textDecorationLine: "line-through",
                                  color: "gray",
                                }
                              : {},
                          ]}
                        >
                          {item.name} :
                        </ThemedText>
                        <ThemedText
                          style={[
                            isDeleted(item.id)
                              ? {
                                  textDecorationLine: "line-through",
                                  color: "gray",
                                  borderColor: "gray",
                                }
                              : {},
                            ,
                            styles.state,
                          ]}
                        >
                          {
                            attributeStates.find(
                              (x) => x.id == item.currentStateId
                            )?.name
                          }
                        </ThemedText>
                      </>
                    )}
                  </ThemedView>
                }
                style={item.cAttributeTypeId == 2 ? styles.itemCollapsable : {}}
              >
                <Animated.View entering={FadeIn}>
                  <ThemedView style={styles.innerItem}>
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
                      {item.description}
                    </ThemedText>
                  </ThemedView>
                  <ThemedView style={styles.options}>
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
                        setEditAttribute(item);
                        router.push("/editattribute");
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
                  </ThemedView>
                </Animated.View>
              </Collapsible>
            </Animated.View>
          )}
          ListEmptyComponent={
            <ThemedView style={Styles.container}>
              <ThemedText>{Resource.get("noattributes")}</ThemedText>
            </ThemedView>
          }
        />
      )}
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  item: {
    width: "100%",
    gap: 5,
  },
  itemCollapsable: {
    borderTopWidth: 2,
    borderBottomWidth: 2,
    borderStyle: "dashed",
    borderColor: "gray",
  },
  itemStateType: {
    width: "100%",
    flexDirection: "row",
    justifyContent: "center",
    alignItems: "center",
    paddingVertical: 10,
    gap: 20,
    flexWrap: "wrap",
  },
  innerItem: {
    paddingHorizontal: "6%",
    paddingVertical: 10,
  },
  text: {
    width: "100%",
  },
  options: {
    paddingHorizontal: 20,
    paddingVertical: 15,
    flexDirection: "row",
    justifyContent: "space-between",
  },
  optionButton: {
    width: "40%",
    flexDirection: "row",
    gap: 10,
  },
  state: {
    padding: 5,
    paddingHorizontal: 10,
    borderWidth: 2,
    borderRadius: 30,
  },
});
