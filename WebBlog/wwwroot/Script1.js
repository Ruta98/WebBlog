// JavaScript source code
var urlApi = "https://localhost:44336/";

function fListArticle() {
    var Data = null;
    axios
        .post(urlApi + "ListArticle", Data
        )
        .then(function (response) {

            add.articles = response.data;

        }).catch(function (error) {
            console.log(error);
        });

}

function fListAddArticle(pId) {
    var Data = { Id: pId };
    axios
        .post(urlApi + "ListAddArticle", Data
        )
        .then(function (response) {

            add.articles.push(...response.data);

        }).catch(function (error) {
            console.log(error);
        });

}

function fDeleteArticle(pId) {
    var Data = { Id: pId };
    axios
        .post(urlApi + "DeleteArticle", Data
        )
        .then(function (response) {
            var res = response.data;
            if (res.state == "Ok")
                fListArticle();
            else
                window.alert("error write article");

        }).catch(function (error) {
            console.log(error);
        });

}
function fAddArticle(pHead, pBody, pAuthorId) {
    var Data =
    {
        Head: pHead,
        Body: pBody,
        AuthorId: pAuthorId
    };
    axios
        .post(urlApi + "AddArticle", Data
        )
        .then(function (response) {

            var res = response.data;
            if (res.state == "Ok")
                fListArticle();
            else
                window.alert("error write article");

        }).catch(function (error) {
            console.log(error);
        });

}

function fEditArticle(pId, pHead, pBody) {
    var Data =
    {
        Id: pId,
        Head: pHead,
        Body: pBody,
        AuthorId: 1
    };
    axios
        .post(urlApi + "UpdateArticle", Data
        )
        .then(function (response) {

            var res = response.data;
            if (res.state == "Ok")
                fListArticle();
            else
                window.alert("error write article");

        }).catch(function (error) {
            console.log(error);
        });

}


function fLogin(pEmail, pPassword) {
    var Data =
    {   Email: pEmail,
        PassWord: pPassword    };
    axios
        .post(urlApi + "Login", Data
        )
        .then(function (response) {

            var res = response.data;
            if (res > 0) {
                add.login = true;
                add.IdUser = res;
            }
            else {
                if (res==-2)
                    window.alert("wrong login");
                if (res == -1)
                    window.alert("wrong password");
            }
        }).catch(function (error) {
            console.log(error);
        });
}


function fAddUser(pEmail, pPassword, pNickname, pName) {
    var Data =
    {
        Email: pEmail,
        PassWord: pPassword,
        NickName: pNickname,
        Name: pName
    };
    axios
        .post(urlApi + "AddUser", Data
        )
        .then(function (response) {

            var res = response.data;
            if (res > 0) {
                add.login = true;
                add.IdUser = res;
                window.alert("registration is successful");

            }
            else {
                if (res == -2)
                    window.alert("Login is used by other user");
                if (res == -1)
                    window.alert("Nickname is used by other user");
            }
        }).catch(function (error) {
            console.log(error);
        });
}



var add = new Vue({
    el: '#add',
    data: {
        title: 'Hello Vue.js!',        
        message: 'text',
        flag: false,
        eflag: false,
        rflag: false,
        articles: [{ id: 13, head: "Head article", body: "Body article", authorId: 2, nickName: "Nataly", time: null, pathPhoto: "Foto\\\\10" }],
        topic: '',
        text: '',
        fId: 0,
        IdUser: 0,
        login: false,
        email: '',
        password: '',
        nickname: '',
        name: '',
    },
    methods: {
        openMessageForm: function () {
            this.text = '';
            this.topic = '';
            this.flag = true;
        },
        eopenMessageForm: function (pId, pHead, pBody) {
            this.text = pBody;
            this.topic = pHead;
            this.eflag = true;
            fId = pId;
        },
        openRegistrationForm: function () {
            this.email = '';
            this.password = '';
            this.nickname = '';
            this.name = '';
            this.rflag = true;
        },
        closeMessageForm: function () {
            this.flag = false;
            this.eflag = false;
            this.rflag = false;
            this.text = '';
            this.topic = '';
            this.email = '';
            this.password = '';
            this.nickname = '';
        },
        addMessageForm: function () {
            fAddArticle(this.topic, this.text, this.IdUser);
            this.text = '';
            this.topic = '';
            this.flag = false;
        },
        editMessageForm: function () {
            fEditArticle(fId, this.topic, this.text);
            fId = 0;
            this.text = '';
            this.topic = '';
            this.eflag = false;
        },
        loadMessage: function () {
           
            fListArticle();
        },

        loadMoreMessage: function () {

            fId = this.articles[this.articles.length - 1].id;
            fListAddArticle(fId);
            fId = 0;
        },
        deleteMessage: function (pId) {
            fId = pId;
            fDeleteArticle(fId);
            fId = 0;
        },
        addUser: function () {
            var flag = true;
            if (this.email == '') {
                flag = false;
                window.alert("enter the login");
            }
            if (this.password == '') {
                flag = false;
                window.alert("enter the password");
            }
            if (this.nickname == '') {
                flag = false;
                window.alert("enter the nickname");
            }
            if (this.name == '') {
                flag = false;
                window.alert("enter the name");
            }
            if (flag) {
                fAddUser(this.email, this.password, this.nickname, this.name);
                this.email = '';
                this.password = '';
                this.nickname = '';
                this.name = '';
                this.rflag = false;
            }
        },
        Login: function () {
            fLogin(this.email, this.password);
        },
        Exit: function () {
            this.login = false;
            this.email = '';
            this.password = '';
        },

    },
     created: function () {

         fListArticle();

    }
})
/*Vue.component('article_form', {
    template: `

      `,
    data() {
        return {
            topic: null,
            text:null
        }
    },
     methods: {
         onSubmit() {
             let articleData = {
                 topic: this.topic,
                 text: this.text
             }
             this.topic = null
             this.text = null
         }
     }
})*/