var config = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }),
    authority: "https://localhost:7010",
    client_id: "client_id_js",
    redirect_uri: "https://localhost:7270/home/signin",
    post_logout_redirect_uri: "https://localhost:7270/Home/Index",
    response_type: "code",
    scope: "openid rc.scope Scope1 Scope2"
};

var userManager = new Oidc.UserManager(config);

var signIn = function () {
    userManager.signinRedirect();
};

var signOut = function () {
    userManager.signoutRedirect();
};

userManager.getUser().then(user => {
    console.log("user:", user);
    if (user) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
    }
});

var callApi = function () {
    axios.get("https://localhost:7050/secret")
        .then(res => {
            console.log(res);
        });
};