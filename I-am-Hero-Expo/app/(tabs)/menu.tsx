import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";
import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import Styles from "@/constants/Styles";
import {
  Platform,
  Pressable,
  ScrollView,
  StyleSheet,
  View,
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import FontAwesome5 from "@expo/vector-icons/FontAwesome5";
import Fontisto from "@expo/vector-icons/Fontisto";
import Entypo from "@expo/vector-icons/Entypo";
import FontAwesome from "@expo/vector-icons/FontAwesome";
import MaterialCommunityIcons from "@expo/vector-icons/MaterialCommunityIcons";
import Ionicons from "@expo/vector-icons/Ionicons";
import FontAwesome6 from "@expo/vector-icons/FontAwesome6";
import { useState } from "react";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Colors } from "@/constants/Colors";
import QuestIcon from "@/icons/QuestIcon";

export default function MenuScreen() {
  const { isToken } = useGlobalContext();
  if (!isToken) return <Auth />;

  const Wrapper = Platform.OS === "web" ? ThemedView : SafeAreaView;
  const [listType, setListType] = useState(false);
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const background = Colors[colorScheme ?? "light"].background;
  const iconsize = 24;
  return (
    <Wrapper style={Styles.container}>
      <ThemedView style={Styles.header} tint={true}>
        <Pressable
          style={({ pressed }) => [
            Styles.headerButtons,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            setListType(!listType);
          }}
        >
          {listType ? (
            <FontAwesome5 name="list" size={24} color={color} />
          ) : (
            <Fontisto name="nav-icon-grid" size={24} color={color} />
          )}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            Styles.headerButtons,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
        >
          <FontAwesome5 name="cog" size={iconsize} color={color} />
        </Pressable>
      </ThemedView>
      <ScrollView
        style={{ width: "100%", backgroundColor: background }}
        contentContainerStyle={{
          width: "100%",
          alignItems: "center",
          gap: 10,
          marginHorizontal: "auto",
          paddingTop: 10,
        }}
      >
        <ThemedView style={styles.profile}>
          <ThemedView style={styles.leftprofile}>
            <ThemedText style={{fontSize:25}}>I am Hero</ThemedText>
            <ThemedText>Level 10</ThemedText>
            <ThemedView style={styles.bar}>
              <ThemedView style={styles.progressbar}></ThemedView>
            </ThemedView>
          </ThemedView>
          <ThemedView style={styles.leftprofile}>
            <FontAwesome6 name="user-astronaut" size={100} color={color} />
          </ThemedView>
        </ThemedView>
        <ThemedView style={[Styles.row, styles.button]}>
          <FontAwesome5 name="scroll" size={iconsize} color={color} />
          <ThemedText>Biography</ThemedText>
        </ThemedView>
        <ThemedView style={[Styles.row, styles.button]}>
          <FontAwesome5 name="dna" size={iconsize} color={color} />
          <ThemedText>Attributes</ThemedText>
        </ThemedView>
        <ThemedView style={[Styles.row, styles.button]}>
          <Entypo name="graduation-cap" size={iconsize} color={color} />
          <ThemedText>Skills</ThemedText>
        </ThemedView>
        <ThemedView style={[Styles.row, styles.button]}>
          <FontAwesome name="check-square" size={iconsize} color={color} />
          <ThemedText>Habits</ThemedText>
        </ThemedView>
        <ThemedView style={[Styles.row, styles.button]}>
          <MaterialCommunityIcons
            name="emoticon-sick"
            size={iconsize}
            color={color}
          />
          <ThemedText>Status effects</ThemedText>
        </ThemedView>
        <ThemedView style={[Styles.row, styles.button]}>
          <FontAwesome5 name="trophy" size={iconsize} color={color} />
          <ThemedText>Achievements</ThemedText>
        </ThemedView>
        <ThemedView style={[Styles.row, styles.button]}>
          <Ionicons name="calendar-sharp" size={iconsize} color={color} />
          <ThemedText>Daylies</ThemedText>
        </ThemedView>
        <ThemedView style={[Styles.row, styles.button]}>
          <QuestIcon size={iconsize} color={color} />
          <ThemedText>Quests</ThemedText>
        </ThemedView>
        <ThemedView style={styles.empty}></ThemedView>
      </ScrollView>
    </Wrapper>
  );
}

const styles = StyleSheet.create({
  box: {
    width: 100,
    height: 100,
    backgroundColor: "red",
  },
  button: {
    width: "60%",
    borderWidth: 2,
    borderRadius: 10,
    padding: 5,
    paddingVertical: 15,
    justifyContent: "center",
  },
  profile: {
    width: "100%",
    height: 200,
    display: "flex",
    flexDirection: "row",
    borderBottomWidth: 2,
    alignItems: "center",
    justifyContent: "center",
  },
  leftprofile: {
    width: "50%",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
    alignItems: "center",
    gap: 10,
  },
  empty: {
    width: "100%",
    height: 100,
  },
  bar: {
    width: '50%',
    height: 30,
    borderWidth: 2,
    borderRadius: 30,
    padding: 2,
  },
  progressbar: {
    width: '80%',
    height: '100%',
    backgroundColor: 'white',
    borderWidth: 2,
    borderRadius: 30,
  },
});
