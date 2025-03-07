import { Collapsible } from "@/components/Collapsible";
import { useGlobalContext } from "@/components/ContextProvider";
import ProgressBar from "@/components/ProgressBar";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Styles from "@/constants/Styles";
import { Attribute } from "@/models/Attribute";
import { useCallback, useEffect, useState } from "react";
import {
  FlatList,
  RefreshControl,
  StyleSheet,
  useColorScheme,
} from "react-native";

export default function AttributesScreen() {
  const { user, api, alert } = useGlobalContext();
  const [attributes, setAttributes] = useState<Attribute[]>(
    user.attributes ?? []
  );
  const [refreshing, setRefreshing] = useState(false);

  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;

  const onRefresh = useCallback(() => {
    setRefreshing(true);
    api
      .GetAttributes()
      .then(() => {
        setAttributes(user.attributes ?? []);
        setRefreshing(false);
      })
      .catch((error) => {
        alert("ERROR", "Something went wrong");
      });
  }, []);

  useEffect(() => {
    setRefreshing(true);
    api
      .GetAttributes()
      .then(() => {
        setAttributes(user.attributes ?? []);
        setRefreshing(false);
      })
      .catch((error) => {
        alert("ERROR", "Something went wrong");
      });
  }, []);
  return (
    <ThemedView style={Styles.container}>
      <FlatList
        refreshControl={
          <RefreshControl refreshing={refreshing} onRefresh={onRefresh} />
        }
        style={{ width: "100%" }}
        contentContainerStyle={{ width: "100%" }}
        data={attributes}
        renderItem={({ index, item }) => (
          <Collapsible
            title={
              <ThemedView key={item.id} style={styles.item}>
                <ThemedText style={styles.text}>{item.name}</ThemedText>
                <ProgressBar
                  minValue={item.minValue ?? 0}
                  curValue={item.value ?? item.minValue ?? 0}
                  maxValue={item.maxValue ?? 0}
                  color={color}
                  height={15}
                  numbersVisible
                  colorfull
                />
              </ThemedView>
            }
          >
            <ThemedView style={styles.innerItem}>
              <ThemedText>{item.description}</ThemedText>
            </ThemedView>
          </Collapsible>
        )}
        ListEmptyComponent={
          <ThemedView style={Styles.container}>
            <ThemedText>You have no attributes yet</ThemedText>
          </ThemedView>
        }
      />
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  item: {
    width: "100%",
    gap: 5
  },
  innerItem:{
    paddingHorizontal:"6%"
  },
  text: {
    width: '100%'
  }
});
