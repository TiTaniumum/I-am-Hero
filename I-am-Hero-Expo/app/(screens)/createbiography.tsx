import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedInput } from "@/components/ThemedInput";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { useState } from "react";
import { Pressable, ScrollView, StyleSheet } from "react-native";

export default function CreateBiographyScreen() {
  const [height, setHeight] = useState(40);
  const { bioText, setBioText, setAlpha, alert, api } = useGlobalContext();
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const styles = createStyle(color);

  function handleClear() {
    setBioText("");
  }

  function handleCreate() {
    api
      .CreateBioPiece(bioText)
      .then(() => {
        setBioText("");
        alert(Resource.get("created!"), "");
      })
      .catch((error) => {
        alert(Resource.get("error!"), Resource.get("somethingwrong"));
      });
  }

  return (
    <ThemedView style={Styles.container}>
      <ScrollView style={styles.scroll}>
        <ThemedInput
          placeholder={Resource.get("typehere")}
          placeholderTextColor="gray"
          style={[styles.input, { height }, { outline: "none" }]}
          multiline
          value={bioText}
          onChangeText={setBioText}
          onContentSizeChange={(e) => {
            setHeight(e.nativeEvent.contentSize.height);
          }}
        />
      </ScrollView>
      <ThemedView style={styles.control}>
        <Pressable
          style={({ hovered, pressed }) => [
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
            Styles.pressable,
            styles.button,
          ]}
          onPress={handleClear}
        >
          <ThemedText>{Resource.get("erase")}</ThemedText>
        </Pressable>
        <Pressable
          style={({ pressed, hovered }) => [
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
            Styles.pressable,
            styles.button,
          ]}
          onPress={handleCreate}
        >
          <ThemedText>{Resource.get("create")}</ThemedText>
        </Pressable>
      </ThemedView>
    </ThemedView>
  );
}

function createStyle(color: any) {
  const styles = StyleSheet.create({
    input: {
      width: "100%",
      borderWidth: 0,
    },
    button: { borderColor: color, width: "40%" },
    scroll: {
      width: "100%",
    },
    control: {
      flexDirection: "row",
      justifyContent: "space-around",
      alignItems: "center",
      width: "100%",
      paddingVertical: 20,
    },
  });
  return styles;
}
