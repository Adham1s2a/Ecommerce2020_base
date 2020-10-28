let english = document.getElementById("english");
let deutsch = document.getElementById("deutsch");
let title = document.getElementById("home");
let contact = document.getElementById("contact");
let login = document.getElementById("login");
let register = document.getElementById("register");
let account = document.getElementById("account");
let features_items = document.getElementById("features items");
let category = document.getElementById("category");
let recommended = document.getElementById("recommended");


recommended

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
        account.innerHTML = "Account";
        features_items.innerHTML = "Features items";
        category.innerHTML = "Category";
        recommended.innerHTML = "recommended items";

    } else if (getlanguage === "deutsch") {

        title.innerHTML = "Home";
        contact.innerHTML = "Kontakt";
        login.innerHTML = "Anmeldung";
        register.innerHTML = "Registrieren";
        account.innerHTML = "Konto";
        features_items.innerHTML = "Top-Artikel";
        category.innerHTML = "Kategorie";
        recommended.innerHTML = "Empfohlene-Artikel";

    }
}


