import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedInput } from "@/components/ThemedInput";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { useLocalSearchParams } from "expo-router";
import { useEffect, useState } from "react";
import { Pressable, ScrollView, StyleSheet } from "react-native";

export default function EditBiographyScreen() {
  const { editBioID, editBioText, setEditBioText, alert, api } = useGlobalContext();
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
        alert("Saved!", "");
      })
      .catch((error) => {
        alert("ERROR", "Something went wrong...");
      });
  }

  return (
    <ThemedView style={Styles.container}>
      <ScrollView style={styles.scroll}>
        <ThemedInput
          placeholder="your biography goes here..."
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
          style={[Styles.pressable, styles.button]}
          onPress={handleClear}
        >
          <ThemedView>
            <ThemedText>Erase</ThemedText>
          </ThemedView>
        </Pressable>
        <Pressable
          style={[Styles.pressable, styles.button]}
          onPress={cancelChanges}
        >
          <ThemedView>
            <ThemedText>Rollback</ThemedText>
          </ThemedView>
        </Pressable>
        <Pressable
          style={[Styles.pressable, styles.button]}
          onPress={handleSave}
        >
          <ThemedView>
            <ThemedText>Save</ThemedText>
          </ThemedView>
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
    button: { borderColor: color, width: "20%" },
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
