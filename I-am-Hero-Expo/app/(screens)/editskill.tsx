import { useGlobalContext } from "@/components/ContextProvider";
import ProgressBar from "@/components/ProgressBar";
import { ThemedInput } from "@/components/ThemedInput";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { Colors } from "@/constants/Colors";
import Resource from "@/constants/Resource";
import Styles from "@/constants/Styles";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { ISkill, ISkillDTO } from "@/models/Skill";
import User from "@/models/User";
import { useEffect, useState } from "react";
import { Pressable, StyleSheet } from "react-native";

export default function EditSkillScreen() {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [experience, setExperience] = useState("");
  const [level, setLevel] = useState("");
  const [curXpProgress, setCurXpProgress] = useState(0);
  const [xpToNextLvlProgress, setXpToNextLvlProgress] = useState(0);

  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;

  const { setAlpha, api, alert, editSkill } = useGlobalContext();

  useEffect(() => {
    setName(editSkill?.name ?? "");
    setDescription(editSkill?.description ?? "");
    setExperience(editSkill?.experience.toString() ?? "");
    setLevel(
      User.GetLevel(
        editSkill?.experience ?? 0,
        editSkill?.cLevelCalculationTypeId ?? 1
      ).toString()
    );
    const { curXp, xpToNextLvl } = User.GetExpPerLevel(
      editSkill?.experience ?? 0,
      1
    );
    setCurXpProgress(curXp);
    setXpToNextLvlProgress(xpToNextLvl);
  }, []);

  function handleLevelChange(value: string) {
    setLevel(value);
    const xp = User.GetExp(+value, 1);
    setExperience(xp.toString());
    const { curXp, xpToNextLvl } = User.GetExpPerLevel(xp, 1);
    setCurXpProgress(curXp);
    setXpToNextLvlProgress(xpToNextLvl);
  }

  function handleExperienceChange(value: string) {
    setExperience(value);
    setLevel(User.GetLevel(+value, 1).toString());
    const { curXp, xpToNextLvl } = User.GetExpPerLevel(+value, 1);
    setCurXpProgress(curXp);
    setXpToNextLvlProgress(xpToNextLvl);
  }

  function Save() {
    const skill: ISkillDTO = {
      id: editSkill?.id,
      name: name,
      description: description,
      experience: +experience,
      cLevelCalculationTypeId: 1,
    };
    api.EditSkill(skill).then((j) => {
      alert(Resource.get("saved!"));
    });
  }

  return (
    <ThemedView
      style={[
        Styles.container,
        { justifyContent: "flex-start", paddingTop: 20, overflow: "scroll" },
      ]}
    >
      <ThemedInput
        value={name}
        onChangeText={setName}
        placeholder={Resource.get("skillname")}
        placeholderTextColor="gray"
        style={styles.input}
      />
      <ThemedInput
        value={description}
        onChangeText={setDescription}
        placeholder={Resource.get("description")}
        placeholderTextColor="gray"
        style={styles.input}
      />
      <ThemedView style={[styles.input, styles.level]}>
        <ThemedInput
          value={level}
          onChangeText={handleLevelChange}
          placeholder={Resource.get("level")}
          placeholderTextColor="gray"
          style={styles.levelInput}
          keyboardType="numeric"
        />
        <ThemedText>lvl = xp</ThemedText>
        <ThemedInput
          value={experience}
          onChangeText={handleExperienceChange}
          placeholder={Resource.get("expamount")}
          placeholderTextColor="gray"
          style={styles.levelInput}
          keyboardType="numeric"
        />
      </ThemedView>
      <ProgressBar
        minValue={0}
        curValue={curXpProgress}
        maxValue={xpToNextLvlProgress}
        numbersVisible
        width="85%"
        height={20}
        color={color}
        type="xp"
        level={+level}
      />
      {name != "" && (
        <Pressable
          style={({ pressed, hovered }) => [
            hovered ? { backgroundColor: setAlpha(color, 0.5) } : {},
            pressed
              ? { backgroundColor: color }
              : { transitionDuration: "0.2s" },
            Styles.pressable,
            { borderColor: color, width: "40%" },
          ]}
          onPress={Save}
        >
          <ThemedText>{Resource.get("save")}</ThemedText>
        </Pressable>
      )}
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  input: { width: "85%" },
  level: {
    flexWrap: "nowrap",
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
  },
  levelInput: {
    width: "40%",
  },
});
