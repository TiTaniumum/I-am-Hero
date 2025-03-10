import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedInput } from "@/components/ThemedInput";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { useState } from "react";
import { Pressable, ScrollView, StyleSheet } from "react-native";

export default function CreateBiographyScreen() {
  const [height, setHeight] = useState(40);
  const {bioText, setBioText} =  useGlobalContext();
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const styles = createStyle(color);
  const {alert, api} = useGlobalContext();

  function handleClear(){
    setBioText("");
  }

  function handleCreate(){
    api.CreateBioPiece(bioText)
    .then(()=>{
      setBioText("");
      alert("Created!", "");
    }).catch((error)=>{
      alert("ERROR","Something went wrong...");
    })
  }

  return (
    <ThemedView style={Styles.container}>
      <ScrollView style={styles.scroll}>
        <ThemedInput
          placeholder="your biography goes here..."
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
        <Pressable style={[Styles.pressable, styles.button]} onPress={handleClear}>
          <ThemedView>
            <ThemedText>Erase</ThemedText>
          </ThemedView>
        </Pressable>
        <Pressable style={[Styles.pressable, styles.button]} onPress={handleCreate}>
          <ThemedView>
            <ThemedText>Create</ThemedText>
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
