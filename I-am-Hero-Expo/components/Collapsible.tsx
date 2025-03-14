import React, { PropsWithChildren, useState } from "react";
import { DimensionValue, StyleSheet, TouchableOpacity, ViewProps, ViewStyle } from "react-native";

import { ThemedText } from "@/components/ThemedText";
import { ThemedView } from "@/components/ThemedView";
import { IconSymbol } from "@/components/ui/IconSymbol";
import { Colors } from "@/constants/Colors";
import { useColorScheme } from "@/hooks/useColorScheme";

export function Collapsible({
  children,
  title,
  style
}: PropsWithChildren & { title: string | React.JSX.Element, style?: ViewStyle}) {
  const [isOpen, setIsOpen] = useState(false);
  const theme = useColorScheme() ?? "light";
  return (
    <ThemedView style={style}>
      <TouchableOpacity
        style={[styles.heading]}
        onPress={() => setIsOpen((value) => !value)}
        activeOpacity={0.8}
      >
        <IconSymbol
          name="chevron.right"
          size={18}
          weight="medium"
          color={theme === "light" ? Colors.light.icon : Colors.dark.icon}
          style={{ transform: [{ rotate: isOpen ? "90deg" : "0deg" }] }}
        />
        {React.isValidElement(title) ? (
          <ThemedView style={styles.outerWidth}>{title}</ThemedView>
        ) : (
          <ThemedText type="defaultSemiBold">{title}</ThemedText>
        )}
        {/* <ThemedText type="defaultSemiBold">{title}</ThemedText> */}
      </TouchableOpacity>
      {isOpen && <ThemedView>{children}</ThemedView>}
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  heading: {
    flexDirection: "row",
    alignItems: "center",
    gap: 6,
  },
  outerWidth:{
    width: '90%'
  }
});
