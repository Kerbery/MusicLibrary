import {
  LIKE_TRACK,
  UNLIKE_TRACK,
  SET_LIKED_TRACKS_IDS,
} from "../actionTypes.js";

const initialState = { ids: [], hasFetched: false };

export default function reduceLikes(state = initialState, action) {
  switch (action.type) {
    case SET_LIKED_TRACKS_IDS: {
      const { likedTracksIds } = action.payload;
      return { ...state, ids: [...likedTracksIds], hasFetched: true };
    }
    case LIKE_TRACK: {
      const { trackId } = action.payload;
      return {
        ...state,
        ids: [...state.ids.filter((id) => id !== trackId), trackId],
      };
    }
    case UNLIKE_TRACK: {
      const { trackId } = action.payload;
      return {
        ...state,
        ids: [...state.ids.filter((id) => id !== trackId)],
      };
    }
    default:
      return state;
  }
}
