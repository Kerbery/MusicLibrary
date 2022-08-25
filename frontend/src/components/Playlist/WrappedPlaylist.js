import { useParams } from "react-router-dom";
import Playlist from "./Playlist";

export default function WrappedPlaylist(props) {
  const { playlistId } = useParams();

  return <Playlist playlistId={playlistId} {...props} />;
}
