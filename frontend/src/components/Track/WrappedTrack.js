import { useParams } from "react-router-dom";
import Track from "./Track";

export default function WrappedTrack(props) {
  const { track } = useParams();

  return <Track permalink={track} {...props} />;
}
