import {
  DimensionValue,
  StyleProp,
  StyleSheet,
  ViewProps,
  ViewStyle,
} from "react-native";
import { ThemedView } from "./ThemedView";
import { ThemedText } from "./ThemedText";
import { useColorScheme } from "@/hooks/useColorScheme.web";
import { Colors } from "@/constants/Colors";

export type ProgressBarProps = ViewProps & {
  minValue: number;
  curValue: number;
  maxValue: number;
  color?: string;
  outerStyle?: StyleProp<ViewStyle>;
  width?: DimensionValue;
  height?: DimensionValue;
  numbersVisible?: boolean;
  colorfull?: boolean;
};

export default function ProgressBar({
  style,
  minValue,
  curValue,
  maxValue,
  color,
  width,
  height,
  numbersVisible,
  colorfull,
}: ProgressBarProps) {
  if (curValue > maxValue) curValue = maxValue;
  if (curValue < minValue) curValue = minValue;
  const progress = (curValue - minValue) / (maxValue - minValue);
  const calculatedWidth: DimensionValue = `${Math.round(progress * 100)}%`;
  const colorScheme = useColorScheme();
  const themeColor: string = Colors[colorScheme ?? "light"].tint;
  const getProgressColor = (progress: number) => {
    if (progress < 0.25) return colorScheme == "light" ? "#FF4D4F" : "#FF6B6B"; // Red
    if (progress < 0.5) return colorScheme == "light" ? "#FFA940" : "#FFAD42"; // Orange
    if (progress < 0.75) return colorScheme == "light" ? "#FADB14" : "#FCE83A"; // Yellow
    return colorScheme == "light" ? "#52C41A" : "#4ADE80"; // Green
  };
  return (
    <ThemedView style={{width}}>
      <ThemedView
        style={[styles.outer, style, { borderColor: color, width: '100%', height }]}
      >
        <ThemedView
          style={[
            styles.inner,
            style,
            {
              width: calculatedWidth,
              backgroundColor: colorfull ? getProgressColor(progress) : color ?? themeColor,
            },
          ]}
        ></ThemedView>
      </ThemedView>
      {numbersVisible && (
        <ThemedView style={styles.numericData}>
          <ThemedText>{minValue}</ThemedText>
          <ThemedText>{curValue}</ThemedText>
          <ThemedText>{maxValue}</ThemedText>
        </ThemedView>
      )}
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  outer: {
    borderWidth: 2,
    borderRadius: 30,
    padding: 3,
    height: 10,
    width: "100%",
  },
  inner: {
    height: "100%",
    borderRadius: 50,
  },
  numericData: {
    flexDirection: "row",
    justifyContent: "space-between",
  },
});
