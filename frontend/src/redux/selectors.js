export const getLikesState = (store) => store.likes;

export const getFetchStatus = (store) => ({
  hasFetched: getLikesState(store).hasFetched,
});

export const getLikeStatus = (store, trackId) => {
  const likes = getLikesState(store).ids;
  return { isLiked: likes.includes(trackId) };
};
