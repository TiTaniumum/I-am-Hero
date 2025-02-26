import Auth from "@/components/Auth";
import { useGlobalContext } from "@/components/ContextProvider";
import QuestIcon from "@/icons/QuestIcon";

export default function QuestsScreen() {
  const { isToken } = useGlobalContext();
  if (!isToken) return <Auth />;
  return (
    <>
      <QuestIcon color="white" size={200} />
    </>
  );
}
