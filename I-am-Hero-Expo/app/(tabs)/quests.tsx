import Auth from "@/components/Auth";
import { Collapsible } from "@/components/Collapsible";
import { useGlobalContext } from "@/components/ContextProvider";
import CreateHero from "@/components/CreateHero";
import ProgressBar from "@/components/ProgressBar";
import Select from "@/components/Select";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import QuestIcon from "@/icons/QuestIcon";
import { IQuestDto, Quest } from "@/models/Quest";
import User from "@/models/User";
import {
  AntDesign,
  FontAwesome,
  Ionicons,
  MaterialIcons,
} from "@expo/vector-icons";
import { router, useFocusEffect } from "expo-router";
import { useCallback, useEffect, useState } from "react";
import {
  ActivityIndicator,
  FlatList,
  Pressable,
  RefreshControl,
  StyleSheet,
  useColorScheme,
  View,
} from "react-native";
import Animated, { FadeIn, FlipInXUp, ZoomOut } from "react-native-reanimated";

export default function QuestsScreen() {
  const { isToken, isHero } = useGlobalContext();

  const { alert, api, user, setAlpha, setEditQuest, common } =
    useGlobalContext();
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const [refreshing, setRefreshing] = useState(false);
  const [loading, setLoading] = useState(false);
  const [quests, setQuests] = useState<Quest[]>([]);
  const [itemsCountdown, setItemsCountdown] = useState<
    {
      id: number;
      countdown: number;
      interval: NodeJS.Timeout | undefined;
      isDeleted: boolean;
    }[]
  >(
    quests.map((item) => {
      return {
        id: item.id,
        countdown: 0,
        interval: undefined,
        isDeleted: false,
      };
    })
  );

  const onRefresh = useCallback(() => {
    if (isToken) {
      setRefreshing(true);
      setLoading(true);
      api
        .GetQuests()
        .then(() => {
          setQuests(user.quests ?? []);
          setRefreshing(false);
          setLoading(false);
        })
        .catch((error) => {});
    }
  }, [isToken]);

  useEffect(() => {
    setItemsCountdown(
      quests.map((item) => {
        return {
          id: item.id,
          countdown: 0,
          interval: undefined,
          isDeleted: false,
        };
      })
    );
  }, [quests]);

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
          .DeleteQuest(id)
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

  const selectedStatus = useCallback(
    (id: number) => {
      return common.questStatuses.find((i) => id == i.id);
    },
    [quests]
  );

  function updateQuestStatus(id: number, statusId: number) {
    const curStatus = quests.find((i) => i.id == id)?.cQuestStatusId ?? 1;
    const xp = quests.find((i) => i.id == id)?.experience ?? 1;
    const quest: IQuestDto = {
      id: id,
      cQuestStatusId: statusId,
      archiveDate: null,
      cDifficultyId: null,
      completionBehaviour: null,
      createDate: null,
      deadline: null,
      description: null,
      experience: null,
      failureBehaviour: null,
      priority: null,
      questLineId: null,
      title: null,
    };
    api.EditQuest(quest).then(() => {
      if (curStatus == 3) {
        if (user.hero)
          api
            .EditHeroExp(user.hero.experience - xp)
            .catch((error) =>
              alert(Resource.get("error!"), Resource.get("somethingwrong"))
            );
      } else if (statusId == 3) {
        if (user.hero)
          api
            .EditHeroExp(user.hero.experience + xp)
            .catch((error) =>
              alert(Resource.get("error!"), Resource.get("somethingwrong"))
            );
      }
      setQuests((previous) =>
        previous.map((item) =>
          item.id == id ? { ...item, cQuestStatusId: statusId } : item
        )
      );
    });
  }

  if (!isToken) return <Auth />;
  if (!isHero) return <CreateHero />;

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
          router.push("/createquest");
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
          data={quests}
          renderItem={({ index, item }) => (
            <Animated.View key={item.id} entering={FlipInXUp} exiting={ZoomOut}>
              <ThemedView
                key={item.id}
                style={[styles.item, { borderColor: color }]}
              >
                <Collapsible
                  title={
                    <ThemedView style={{ width: "100%", alignItems: "center" }}>
                      <ThemedText
                        style={[
                          styles.itemName,
                          isDeleted(item.id)
                            ? {
                                textDecorationLine: "line-through",
                                color: "gray",
                              }
                            : {},
                        ]}
                      >
                        {item.title}
                      </ThemedText>
                    </ThemedView>
                  }
                >
                  <Animated.View entering={FadeIn}>
                    <ThemedText
                      style={[
                        styles.text,
                        isDeleted(item.id)
                          ? {
                              textDecorationLine: "line-through",
                              color: "gray",
                            }
                          : {},
                      ]}
                    >
                      {Resource.get("experience")}: +{item.experience}
                    </ThemedText>
                    <ThemedText
                      style={[
                        styles.text,
                        isDeleted(item.id)
                          ? {
                              textDecorationLine: "line-through",
                              color: "gray",
                            }
                          : {},
                      ]}
                    >
                      {item.description}
                    </ThemedText>
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
                          styles.suboptionButton,
                          isDeleted(item.id) || item.cQuestStatusId === 3
                            ? { borderColor: "gray" }
                            : {},
                        ]}
                        onPress={() => {
                          if (isDeleted(item.id) || item.cQuestStatusId === 3)
                            return;
                          updateQuestStatus(item.id, 3);
                        }}
                      >
                        <FontAwesome
                          name="check"
                          size={24}
                          color={
                            isDeleted(item.id) || item.cQuestStatusId === 3
                              ? "gray"
                              : color
                          }
                        />
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
                          styles.suboptionButton,
                          isDeleted(item.id) || item.cQuestStatusId === 4
                            ? { borderColor: "gray" }
                            : {},
                        ]}
                        onPress={() => {
                          if (isDeleted(item.id) || item.cQuestStatusId === 4)
                            return;
                          updateQuestStatus(item.id, 4);
                        }}
                      >
                        <FontAwesome
                          name="remove"
                          size={24}
                          color={
                            isDeleted(item.id) || item.cQuestStatusId === 4
                              ? "gray"
                              : color
                          }
                        />
                      </Pressable>
                      <Select
                        data={common.questStatuses}
                        displayTitle={(i) => Resource.extract(i)}
                        getTitle={(i) => Resource.extract(i)}
                        onSelect={(i) => {
                          updateQuestStatus(item.id, i.id);
                        }}
                        selectedValue={selectedStatus(
                          item.cQuestStatusId ?? -1
                        )}
                        style={{ width: "40%" }}
                        displayStyle={{ height: 50 }}
                        color={isDeleted(item.id) ? "gray" : color}
                        isActive={!isDeleted(item.id)}
                      />
                    </View>
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
                          setEditQuest(item);
                          router.push("/editquest");
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
              </ThemedView>
            </Animated.View>
          )}
          ListEmptyComponent={
            <ThemedView style={Styles.container}>
              <QuestIcon color={color} size={200} />
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
    padding: 10,
    width: "100%",
    borderBottomWidth: 0,
  },
  itemName: {
    fontWeight: "700",
  },
  date: {
    color: "gray",
  },
  text: {
    paddingTop: 20,
    paddingHorizontal: 20,
  },
  options: {
    paddingTop: 15,
    flexDirection: "row",
    justifyContent: "space-between",
  },
  optionButton: {
    width: "40%",
    flexDirection: "row",
    gap: 10,
  },
  suboptionButton: {
    width: "25%",
    flexDirection: "row",
  },
});
