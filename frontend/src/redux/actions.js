import playlistsService from "../services/playlists.service";
import { LIKE_TRACK, UNLIKE_TRACK, SET_LIKED_TRACKS_IDS } from "./actionTypes";
import { getFetchStatus } from "./selectors";

export const likeTrack = (trackId) => async (dispatch) => {
  const success = await playlistsService.likeTrack(trackId);
  if (success) {
    dispatch({
      type: LIKE_TRACK,
      payload: { trackId },
    });
  }
};

export const unlikeTrack = (trackId) => async (dispatch) => {
  const success = await playlistsService.unlikeTrack(trackId);
  if (success) {
    dispatch({
      type: UNLIKE_TRACK,
      payload: { trackId },
    });
  }
};

export const fetchLikesIds = (userPermalink) => async (dispatch, getState) => {
  const { hasFetched } = getFetchStatus(getState());
  if (!hasFetched) {
    const likedTracksIds = await playlistsService.getAllLikedTracksIds(
      userPermalink
    );
    dispatch({
      type: SET_LIKED_TRACKS_IDS,
      payload: { likedTracksIds },
    });
  }
};
