import Svg, { Path } from 'react-native-svg';

const TempIcon = ({ color = 'black', size = 28 }) => (
  <Svg height={size} width={size} viewBox='0 0 400 400' fill="none" stroke={color} strokeWidth="25" strokeLinecap="round" strokeLinejoin="round">
    <Path d="M170 110 L80 120 L200 370 L320 120 L230 110"/>
    <Path d="M200 20 L160 90 L200 170 L240 90 Z"/>
  </Svg>
);

export default TempIcon;
