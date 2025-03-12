import { View, type ViewProps } from 'react-native';

import { useThemeColor } from '@/hooks/useThemeColor';
import { tintColorDark, tintColorLight } from '@/constants/Colors';

export type ThemedViewProps = ViewProps & {
  lightColor?: string;
  darkColor?: string;
  tint?: boolean;
};

export function ThemedView({ style, lightColor, darkColor, tint, ...otherProps }: ThemedViewProps) {
  const backgroundColor = tint? useThemeColor({ light: lightColor, dark: darkColor }, 'backgroundTint') : useThemeColor({ light: lightColor, dark: darkColor }, 'background');
  const color = useThemeColor({ light: lightColor, dark: darkColor }, 'text');
  return <View style={[{backgroundColor}, {borderColor: color}, style]} {...otherProps} />;
}
