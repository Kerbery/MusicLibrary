import { useParams } from "react-router-dom";
import PLaylist from "./Playlist";

export default function WrappedPlaylist(props) {
  const { playlistId } = useParams();

  return <PLaylist playlistId={playlistId} {...props} />;
}
