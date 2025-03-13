import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedInput } from "@/components/ThemedInput";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { useLocalSearchParams } from "expo-router";
import { useEffect, useState } from "react";
import { Pressable, ScrollView, StyleSheet } from "react-native";

export default function EditBiographyScreen() {
  const { editBioID, editBioText, setEditBioText, alert, api, setAlpha } =
    useGlobalContext();
  const [height, setHeight] = useState(40);
  const [backupText, setBackupText] = useState<string>("");
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const styles = createStyle(color);

  useEffect(() => {
    setBackupText(editBioText);
  }, []);

  function handleClear() {
    setEditBioText("");
  }

  function cancelChanges() {
    setEditBioText(backupText);
  }

  function handleSave() {
    api
      .EditBioPiece(editBioID, editBioText)
      .then(() => {
        alert(Resource.get("saved!"));
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
          value={editBioText}
          onChangeText={setEditBioText}
          onContentSizeChange={(e) => {
            setHeight(e.nativeEvent.contentSize.height);
          }}
        />
      </ScrollView>
      <ThemedView style={styles.control}>
        <Pressable
          style={({ pressed, hovered }) => [
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
          style={({ hovered, pressed }) => [
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
            Styles.pressable,
            styles.button,
          ]}
          onPress={cancelChanges}
        >
          <ThemedText>{Resource.get("rollback")}</ThemedText>
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
          onPress={handleSave}
        >
          <ThemedText>{Resource.get("save")}</ThemedText>
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
    button: { borderColor: color, width: "30%" },
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
