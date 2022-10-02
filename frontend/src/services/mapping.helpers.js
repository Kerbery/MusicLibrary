export function getTrackData(track) {
  const user = {
    username: track.user.userName,
    permalink: track.user.permalink,
    url: `/${track.user.permalink}`,
  };

  const trackData = {
    title: track.title,
    artworkUrl: track.artworkUrl || "",
    url: `/${track.user.permalink}/${track.permalink}`,
    user,
  };
  return trackData;
}

export function getTrackMediaData(track) {
  const user = {
    username: track.user.userName,
    permalink: track.user.permalink,
    avatarUrl: track.user.avatarUrl,
    url: `/${track.user.permalink}`,
  };

  const trackData = {
    title: track.title,
    artworkUrl: track.artworkUrl || "",
    url: `/${track.user.permalink}/${track.permalink}`,
    mediaUrl: track.mediaUrl,
    user,
  };
  return trackData;
}

export function getPlaylistData(playlist) {
  const user = {
    username: playlist.user.userName,
    permalink: playlist.user.permalink,
  };

  const playlistData = {
    id: playlist.playlistId,
    title: playlist.title,
    artworkUrl: playlist.items?.[0]?.getTrackDTO?.artworkUrl || "",
    url: `/${playlist.user.permalink}/playlists/${playlist.playlistId}`,
    items: playlist.items,
    user,
  };
  return playlistData;
}

export function getTrackDataFromItem(item) {
  return getTrackData(item.track);
}

export function getPlaylistDataFromItem(item) {
  return getPlaylistData(item.playlist);
}
