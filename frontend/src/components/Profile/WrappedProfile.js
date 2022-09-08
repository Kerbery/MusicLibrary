import { useParams } from "react-router-dom";
import Profile from "./Profile";

export default function WrappedProfile(props) {
  const { user } = useParams();

  return <Profile user={user} {...props} />;
}
