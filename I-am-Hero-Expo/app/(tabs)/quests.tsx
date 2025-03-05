import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";
import QuestIcon from "@/icons/QuestIcon";
import { useEffect } from "react";

export default function QuestsScreen() {
  const { isToken, alert, api } = useGlobalContext();
  if (!isToken) return <Auth />;
  useEffect(()=>{
    alert("it is working!","");
  },[])
  return (
    <>
      <QuestIcon color="white" size={200} />
    </>
  );
}
