import { useParams } from "react-router-dom";
import Track from "./Track";

export default function WrappedTrack(props) {
  const { id } = useParams();

  return <Track id={id} {...props} />;
}
