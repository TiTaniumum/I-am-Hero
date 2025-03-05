import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import Styles from "@/constants/Styles";
import { FlatList, Pressable, StyleSheet } from "react-native";
import AntDesign from "@expo/vector-icons/AntDesign";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Colors } from "@/constants/Colors";
import { router } from "expo-router";
import { useEffect, useState } from "react";
import { BioPiece } from "@/models/BioPiece";
import { useGlobalContext } from "@/components/ContextProvider";
import { Collapsible } from "@/components/Collapsible";
import MaterialIcons from "@expo/vector-icons/MaterialIcons";

export default function BiographyScreen() {
  const { user, api } = useGlobalContext();
  const [bioPieces, setBioPieces] = useState<BioPiece[]>(user.biopieces ?? []);
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const background = Colors[colorScheme ?? "light"].background;
  useEffect(() => {
    api.GetBioPieces().then(() => {
      setBioPieces(user.biopieces ?? []);
    });
  }, []);
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
      <FlatList
        style={styles.listcontainer}
        contentContainerStyle={styles.listcontainer}
        data={bioPieces}
        renderItem={({ index, item }) => (
          <ThemedView key={item.id} style={styles.item}>
            <Collapsible
              title={
                <ThemedText style={styles.date}>
                  {item.createDate instanceof Date
                    ? item.createDate.toLocaleDateString()
                    : new Date(item.createDate).toLocaleDateString()}
                </ThemedText>
              }
            >
              <ThemedView style={styles.options}>
                <Pressable style={[Styles.pressable, { borderColor: color }, styles.optionButton]}>
                  <MaterialIcons name="mode-edit" size={24} color={color} />
                  <ThemedText>Edit</ThemedText>
                </Pressable>
                <Pressable style={[Styles.pressable, { borderColor: color }, styles.optionButton]}>
                  <MaterialIcons name="delete" size={24} color={color} />
                  <ThemedText>Delete</ThemedText>
                </Pressable>
              </ThemedView>
            </Collapsible>
            <ThemedText style={styles.text}>{item.text}</ThemedText>
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
    flexDirection:'row',
    justifyContent: 'space-between'
  },
  optionButton:{
    width: '40%',
    flexDirection:'row',
    gap: 10
  }
});
