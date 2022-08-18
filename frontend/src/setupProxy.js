const { createProxyMiddleware } = require("http-proxy-middleware");

const context = ["/api/Playlists", "/api/PlaylistItems"];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: "https://localhost:8081",
    secure: false,
  });

  app.use(appProxy);
};
