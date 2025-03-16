import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedInput } from "@/components/ThemedInput";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { IQuestDto } from "@/models/Quest";
import { useState } from "react";
import { Pressable, StyleSheet } from "react-native";
import Animated, { FlipInXUp, FlipInYLeft, FlipInYRight, FlipOutYLeft, FlipOutYRight, withDecay } from "react-native-reanimated";

export default function CreateQuestScreen() {
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [xp, setXp] = useState("");

  const [isSimple, setIsSimple] = useState(true);
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;

  const { setAlpha, api, alert } = useGlobalContext();

  function Create() {
    const quest: IQuestDto = {
      title: title,
      description: description,
      experience: +xp, 
      archiveDate: null,
      cDifficultyId: null,
      completionBehaviour: null,
      cQuestStatusId: 1,
      createDate: null,
      deadline: null,
      failureBehaviour: null,
      priority: null,
      questLineId: null,
    };
    api
      .CreateQuest(quest)
      .then((j) => {
        alert(Resource.get("created!"));
      })
      .catch((error) => {
        alert(Resource.get("error!"), Resource.get("somthingwrong"));
      });
  }

  return (
    <ThemedView
      style={[
        Styles.container,
        { justifyContent: "flex-start", overflow: "scroll" },
      ]}
    >
      <ThemedView style={{ flexDirection: "row", width: "100%" }}>
        <Pressable
          style={({ pressed, hovered }) => [
            styles.headerButton,
            {
              width: "50%",
              backgroundColor: isSimple ? setAlpha(color, 0.2) : "none",
            },
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
          ]}
          onPress={() => {
            setIsSimple(true);
          }}
        >
          <ThemedText>{Resource.get("simple")}</ThemedText>
        </Pressable>
        <Pressable
          style={({ pressed, hovered }) => [
            styles.headerButton,
            {
              width: "50%",
              backgroundColor: isSimple ? "none" : setAlpha(color, 0.2),
            },
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
          ]}
          onPress={() => {
            setIsSimple(false);
          }}
        >
          <ThemedText>{Resource.get("detailed")}</ThemedText>
        </Pressable>
      </ThemedView>
      {isSimple && (
        <Animated.View entering={FlipInYLeft} exiting={FlipOutYLeft} style={{width: '100%', alignItems:"center", gap: 10}}>
          <ThemedInput
            value={title}
            onChangeText={setTitle}
            placeholder={Resource.get("title")}
            placeholderTextColor="gray"
            style={styles.input}
          />
          <ThemedInput
            value={xp}
            onChangeText={setXp}
            placeholder={Resource.get("xpquest")}
            placeholderTextColor="gray"
            style={styles.input}
            keyboardType="numeric"
          />
        </Animated.View>
      )}
      {!isSimple && (
        <Animated.View entering={FlipInYRight} exiting={FlipOutYRight} style={{width: '100%', alignItems:"center", gap: 10}}>
          <ThemedInput
            value={title}
            onChangeText={setTitle}
            placeholder={Resource.get("title")}
            placeholderTextColor="gray"
            style={styles.input}
          />
          <ThemedInput
            value={description}
            onChangeText={setDescription}
            placeholder={Resource.get("description")}
            placeholderTextColor="gray"
            style={[styles.input, { height: 100 }]}
            multiline
          />
          <ThemedInput
            value={xp}
            onChangeText={setXp}
            placeholder={Resource.get("xpquest")}
            placeholderTextColor="gray"
            style={styles.input}
            keyboardType="numeric"
          />
        </Animated.View>
      )}
      {title != '' && (
        <Pressable
        style={({ pressed, hovered }) => [
          hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
          pressed
            ? { backgroundColor: color }
            : { transitionDuration: "0.2s" },
          Styles.pressable,
          { borderColor: color,width: "40%" },
        ]}
        onPress={Create}
      >
        <ThemedText>{Resource.get("create")}</ThemedText>
      </Pressable>
      )}
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  input: { width: "85%" },
  headerButton: {
    padding: 5,
    overflow: "hidden",
    justifyContent: "center",
    alignItems: "center",
  },
});
