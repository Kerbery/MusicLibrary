import axios from "axios";
import AuthService from "./auth.service";

const PLAYLISTS_API_URL = "/gateway/Playlists";
const PLAYLIST_ITEMS_API_URL = "/gateway/PlaylistItems";

class PlaylistService {
  async getPlaylists(userId, PageNumber = 1, PageSize = 24) {
    let token = await AuthService.getAccessToken();
    let params = new URLSearchParams({
      //userId,
      PageNumber,
      PageSize,
    });
    return axios.get(`${PLAYLISTS_API_URL}?${params}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  async getPlaylist(playlistId) {
    let token = await AuthService.getAccessToken();
    return axios.get(`${PLAYLISTS_API_URL}/${playlistId}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  async getPlaylistItems(playlistId, PageNumber = 1, PageSize = 24) {
    let token = await AuthService.getAccessToken();
    let params = new URLSearchParams({
      playlistId,
      PageNumber,
      PageSize,
    });
    const path = `${PLAYLIST_ITEMS_API_URL}?${params}`;
    return axios.get(path, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }
}
export default new PlaylistService();
