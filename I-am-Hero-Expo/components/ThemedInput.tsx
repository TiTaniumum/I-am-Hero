import {
  StyleSheet,
  TextInput,
  TextInputProps,
  View,
  type ViewProps,
} from "react-native";

import { useThemeColor } from "@/hooks/useThemeColor";
import { Colors } from "@/constants/Colors";
import { useColorScheme } from "@/hooks/useColorScheme.web";

export type ThemedInputProps = TextInputProps & {
  lightColor?: string;
  darkColor?: string;
};

export function ThemedInput({
  style,
  lightColor,
  darkColor,
  ...otherProps
}: ThemedInputProps) {
  const backgroundColor = useThemeColor(
    { light: lightColor, dark: darkColor },
    "background"
  );
  const colorScheme = useColorScheme();
    const color = Colors[colorScheme ?? "light"].tint;
  return (
    <TextInput
      style={[
        styles.input,
        { backgroundColor },
        { color },
        style,
        { borderColor: color },
      ]}
      {...otherProps}
    />
  );
}

const styles = StyleSheet.create({
  input: {
    borderWidth: 2,
    borderRadius: 10,
    padding: 10,
  },
});
