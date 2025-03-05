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
} from "react-native";
import { SafeAreaView } from "react-native-safe-area-context";
import FontAwesome5 from "@expo/vector-icons/FontAwesome5";
import Fontisto from "@expo/vector-icons/Fontisto";
import Entypo from "@expo/vector-icons/Entypo";
import MaterialCommunityIcons from "@expo/vector-icons/MaterialCommunityIcons";
import FontAwesome6 from "@expo/vector-icons/FontAwesome6";
import AntDesign from "@expo/vector-icons/AntDesign";
import { useState } from "react";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Colors } from "@/constants/Colors";
import QuestIcon from "@/icons/QuestIcon";
import { router } from "expo-router";

export default function MenuScreen() {
  const { isToken,user } = useGlobalContext();
  
  const Wrapper = Platform.OS === "web" ? ThemedView : SafeAreaView;
  const [listType, setListType] = useState(false);
  const [isHover, setIsHover] = useState(false);
  const colorScheme = useColorScheme();
  const color = Colors[colorScheme ?? "light"].tint;
  const background = Colors[colorScheme ?? "light"].background;
  const iconsize = 24 * (listType ? 1 : 2);
  const styles = getStyles(listType, color, background, iconsize)
  
  if (!isToken) return <Auth />;
  return (
    <Wrapper style={[Styles.container,{gap: 0}]}>
      <ThemedView style={Styles.header} tint={true}>
        <Pressable
          onHoverIn={()=>{setIsHover(true);}}
          onHoverOut={()=>{setIsHover(false);}}
          style={({ pressed }) => [
            Styles.headerButtons,
            isHover ? {backgroundColor: "rgba(255, 255, 255, 0.5)"} : {},
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
          onPress={()=>{router.push('/settings')}}
        >
          <FontAwesome5 name="cog" size={24} color={color} />
        </Pressable>
      </ThemedView>
      <ScrollView
        style={{ width: "100%", backgroundColor: background }}
        contentContainerStyle={listType ? styles.menulist : styles.menugrid}
      >
        <ThemedView style={styles.profile}>
          <ThemedView style={styles.leftprofile}>
            <ThemedText style={{ fontSize: 25 }}>I am {user.hero?.name}</ThemedText>
            <ThemedText>Level 10</ThemedText>
            <ThemedView style={styles.bar}>
              <ThemedView style={styles.progressbar}></ThemedView>
            </ThemedView>
          </ThemedView>
          <ThemedView style={styles.leftprofile}>
            <FontAwesome6 name="user-astronaut" size={100} color={color} />
          </ThemedView>
        </ThemedView>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/biography");
          }}
        >
          <FontAwesome5 name="scroll" size={iconsize} color={color} />
          {listType && <ThemedText>Biography</ThemedText>}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/attributes");
          }}
        >
          <FontAwesome5 name="dna" size={iconsize} color={color} />
          {listType && <ThemedText>Attributes</ThemedText>}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/skills");
          }}
        >
          <Entypo name="graduation-cap" size={iconsize} color={color} />
          {listType && <ThemedText>Skills</ThemedText>}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/habbits");
          }}
        >
          <MaterialCommunityIcons name="progress-check" size={iconsize} color={color} />
          {listType && <ThemedText>Habits</ThemedText>}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/statuseffects");
          }}
        >
          <MaterialCommunityIcons
            name="heart-pulse"
            size={iconsize}
            color={color}
          />
          {listType && <ThemedText>Status effects</ThemedText>}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/achievements");
          }}
        >
          <MaterialCommunityIcons name="trophy" size={iconsize} color={color} />
          {listType && <ThemedText>Achievements</ThemedText>}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/calendars");
          }}
        >
          <MaterialCommunityIcons name="calendar-check" size={iconsize} color={color} />
          {listType && <ThemedText>Daylies</ThemedText>}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/quests");
          }}
        >
          <QuestIcon size={iconsize} color={color} />
          {listType && <ThemedText>Quests</ThemedText>}
        </Pressable>
        <Pressable
          style={({ pressed }) => [
            styles.button,
            pressed ? Styles.pressablePressed : Styles.pressableNotPressed,
          ]}
          onPress={() => {
            router.push("/social");
          }}
        >
          <AntDesign name="message1" size={iconsize} color={color} />
          {listType && <ThemedText>Social</ThemedText>}
        </Pressable>
        <ThemedView style={styles.empty}></ThemedView>
      </ScrollView>
    </Wrapper>
  );
}

function getStyles(listType: boolean, color: string, background: string, iconsize: number){
  const styles = StyleSheet.create({
    box: {
      width: 100,
      height: 100,
      backgroundColor: "red",
    },
    button: {
      width: listType ? "60%" : iconsize*2,
      borderWidth: 2,
      borderRadius: 10,
      padding: 5,
      paddingVertical: 15,
      justifyContent: "center",
      borderColor: color,
      display: 'flex',
      flexDirection: 'row',
      gap: 10
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
      width: "70%",
      height: 30,
      borderWidth: 2,
      borderRadius: 30,
      padding: 2,
    },
    progressbar: {
      width: "80%",
      height: "100%",
      backgroundColor: "white",
      borderWidth: 2,
      borderRadius: 30,
    },
    menulist: {
      width: "100%",
      alignItems: "center",
      gap: 10,
      marginHorizontal: "auto",
    },
    menugrid: {
      gap: 25,
      display: "flex",
      flexDirection: "row",
      alignItems: "center",
      justifyContent: "center",
      flexWrap: "wrap",
    },
    scrollview: { width: "100%", backgroundColor: background }
  });
  return styles
}

