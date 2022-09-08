import { useParams } from "react-router-dom";
import GridPlaylist from "./GridPlaylist";

export default function WrappedGridPlaylist(props) {
  let { user } = useParams();
  let { next, ...rest } = props;

  let newNext = (...args) => {
    return next(user, ...args);
  };

  return <GridPlaylist {...rest} next={newNext} />;
}
