import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import Styles from "@/constants/Styles";
import { FlatList, Pressable, StyleSheet } from "react-native";
import AntDesign from "@expo/vector-icons/AntDesign";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Colors } from "@/constants/Colors";

const mock = [
  {
    id: 1,
    text: "aslkdjfklsadjf",
    createdate: new Date(),
  },
  {
    id: 2,
    text: "aslkdjfklsadjf",
    createdate: new Date(),
  },
  {
    id: 3,
    text: "aslkdjfklsadjf",
    createdate: new Date(),
  },
];

export default function BiographyScreen() {
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const background = Colors[colorScheme ?? "light"].background;
  return (
    <ThemedView style={Styles.container}>
      <Pressable
        style={({ pressed }) => [
          { position: "absolute", bottom: 20, right: 20, borderRadius: "100%" },
          pressed
            ? { backgroundColor: "white" }
            : { transitionDuration: "0.2s" },
        ]}
      >
        <AntDesign name="pluscircleo" size={50} color={color} />
      </Pressable>
      <FlatList
        style={styles.listcontainer}
        contentContainerStyle={styles.listcontainer}
        data={mock}
        renderItem={({ index, item }) => (
          <ThemedView key={item.id} style={styles.item}>
            <ThemedText>{item.createdate.toLocaleDateString()}</ThemedText>
            <ThemedText>{item.text}</ThemedText>
          </ThemedView>
        )}
      />
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  listcontainer: {
    width: "100%",
  },
  item: {
    width: "100%",
    borderBottomWidth: 1,
    borderBottomColor: "white",
  },
  date: {},
});
