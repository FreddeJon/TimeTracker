export const config = {
  client_id: "ReactClient",
  automaticSilentRenew: true,
  redirect_uri: "http://localhost:3000/signin-oidc",
  post_logout_redirect_uri: "http://localhost:3000/signout-oidc",
  responseType: "token id_token",
  scope: "openid profile roles user_scope",
  authority: "https://localhost:5001",
};
