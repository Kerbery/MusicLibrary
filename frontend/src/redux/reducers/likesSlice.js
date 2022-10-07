import { createSlice } from "@reduxjs/toolkit";
import authService from "../../services/auth.service";
import playlistsService from "../../services/playlists.service";

const initialState = {
  ids: [],
  hasFetched: false,
};

export const likesSlice = createSlice({
  name: "likes",
  initialState,
  reducers: {
    like: (state, action) => {
      const { trackId } = action.payload;
      if (!state.ids.includes(trackId)) {
        state.ids = [trackId, ...state.ids];
      }
    },
    unlike: (state, action) => {
      const { trackId } = action.payload;
      state.ids = state.ids.filter((id) => id !== trackId);
    },
    setLikes: (state, action) => {
      const { likedTracksIds } = action.payload;
      state.ids = likedTracksIds;
      state.hasFetched = true;
    },
  },
});

export const likeTrack = (trackId) => async (dispatch) => {
  const success = await playlistsService.likeTrack(trackId);
  if (success) {
    dispatch(like({ trackId }));
  }
};

export const unlikeTrack = (trackId) => async (dispatch) => {
  const success = await playlistsService.unlikeTrack(trackId);
  if (success) {
    dispatch(unlike({ trackId }));
  }
};

export const fetchLikesIds = () => async (dispatch, getState) => {
  const { hasFetched } = selectFetchState(getState());
  if (!hasFetched) {
    const username = authService.user.preferred_name;
    const likedTracksIds = await playlistsService.getAllLikedTracksIds(
      username
    );
    dispatch(setLikes({ likedTracksIds }));
  }
};

export const selectLikesState = (store) => store.likes;

export const selectFetchState = (store) => ({
  hasFetched: selectLikesState(store).hasFetched,
});

export const selectLikeStatus = (store, trackId) => {
  const likes = selectLikesState(store).ids;
  return { isLiked: likes.includes(trackId) };
};

export const { like, unlike, setLikes } = likesSlice.actions;

export default likesSlice.reducer;
