export function getTrackData(track) {
  var gridItemData = {};
  gridItemData.title = track.title;
  gridItemData.user = {
    username: track.user.userName,
    permalink: track.user.permalink,
  };

  gridItemData.artworkUrl = track.artworkUrl || "";
  gridItemData.url = `/${track.user.permalink}/${track.permalink}`;
  return gridItemData;
}

export function getPlaylistData(playlist) {
  var gridItemData = {};
  gridItemData.title = playlist.title;
  gridItemData.user = {
    username: playlist.user.userName,
    permalink: playlist.user.permalink,
  };
  gridItemData.artworkUrl = playlist.items?.[0]?.getTrackDTO?.artworkUrl || "";
  gridItemData.url = `/${playlist.user.permalink}/playlists/${playlist.playlistId}`;
  return gridItemData;
}

export function getTrackDataFromItem(item) {
  return getTrackData(item.track);
}

export function getPlaylistDataFromItem(item) {
  return getPlaylistData(item.playlist);
}
