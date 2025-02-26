import { Tabs } from "expo-router";
import React from "react";
import { Platform } from "react-native";

import { HapticTab } from "@/components/HapticTab";
import { IconSymbol } from "@/components/ui/IconSymbol";
import TabBarBackground from "@/components/ui/TabBarBackground";
import { Colors } from "@/constants/Colors";
import { useColorScheme } from "@/hooks/useColorScheme";
import AntDesign from "@expo/vector-icons/AntDesign";
import Ionicons from "@expo/vector-icons/Ionicons";

import Entypo from '@expo/vector-icons/Entypo';
import QuestIcon from "@/icons/QuestIcon";

export default function TabLayout() {
  const colorScheme = useColorScheme();
  const iconsize = 30;
  return (
    <Tabs
      screenOptions={{
        tabBarActiveTintColor: Colors[colorScheme ?? "light"].tint,
        headerShown: false,
        tabBarButton: HapticTab,
        tabBarBackground: TabBarBackground,
        tabBarStyle: Platform.select({
          ios: {
            position: "absolute",
          },
          default: {},
        }),
      }}
    >
      <Tabs.Screen
        name="index"
        options={{
          title: "",
          tabBarIcon: ({ color }) => (
            <IconSymbol size={iconsize} name="house.fill" color={color} />
          ),
        }}
      />
      <Tabs.Screen
        name="social"
        options={{
          title: "",
          tabBarIcon: ({ color }) => (
            <AntDesign name="message1" size={iconsize} color={color} />
          ),
        }}
      />
      <Tabs.Screen
        name="calendars"
        options={{
          title: "",
          tabBarIcon: ({ color }) => (
            <Ionicons name="calendar-sharp" size={iconsize} color={color} />
          ),
        }}
      />
      <Tabs.Screen
        name="quests"
        options={{
          title: "",
          tabBarIcon: ({ color }) => (
            <QuestIcon color={color} size={iconsize}/>
          ),
        }}
      />
      <Tabs.Screen
        name="menu"
        options={{
          title: "",
          tabBarIcon: ({ color }) => (
            <Entypo name="grid" size={iconsize} color={color} />
          ),
        }}
      />
    </Tabs>
  );
}
