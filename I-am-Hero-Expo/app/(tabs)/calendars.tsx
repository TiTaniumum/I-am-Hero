import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";

export default function CalendarScreen() {
  const {isToken} = useGlobalContext();
  if(!isToken)
      return <Auth/>
  return <></>;
}