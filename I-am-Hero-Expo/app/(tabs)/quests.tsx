import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";
import TempIcon from "@/components/TempIcon";

export default function QuestsScreen() {
  const {isToken} = useGlobalContext();
  if(!isToken)
      return <Auth/>
  return <>
    <TempIcon color="white" size={200}/>
  </>;
}
