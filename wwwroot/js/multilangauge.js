let english = document.getElementById("english");
let deutsch = document.getElementById("deutsch");
let title = document.getElementById("home");
let contact = document.getElementById("contact");
let login = document.getElementById("login");
let register = document.getElementById("register");

english.onclick = () => {
    setLanguage("english");
    localStorage.setItem("lang", "english");



};

deutsch.onclick = () => {

    setLanguage("deutsch");
    localStorage.setItem("lang", "deutsch");

};

onload = () => {

    setLanguage(localStorage.getItem("lang"));

}


function setLanguage(getlanguage) {

    if (getlanguage === "english") {

        title.innerHTML = "Home";
        contact.innerHTML = "Contact";
        login.innerHTML = "login";
        register.innerHTML = "Register";

    } else if (getlanguage === "deutsch") {

        title.innerHTML = "zu Hause";
        contact.innerHTML = "Kontakt";
        login.innerHTML = "Anmeldung";
        register.innerHTML = "Registrieren";

    }
}


