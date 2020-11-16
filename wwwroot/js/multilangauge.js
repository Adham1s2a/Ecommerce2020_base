let english = document.getElementById("english");
let deutsch = document.getElementById("deutsch");
let france = document.getElementById("france");
let title = document.getElementById("home");
let contact = document.getElementById("contact");
let login = document.getElementById("login");
let register = document.getElementById("register");
let account = document.getElementById("account");
let features_items = document.getElementById("features items");
let category = document.getElementById("category");
let recommended = document.getElementById("recommended");
let checkout = document.getElementById("checkout");
let cart = document.getElementById("cart");
let logout = document.getElementById("logout");
let productdetail = document.getElementById("productDetail");
let productname = document.getElementById("pname");
let productprice = document.getElementById("pprice");
let productdesc = document.getElementById("pdescription");




english.onclick = () => {
    setLanguage("english");
    localStorage.setItem("lang", "english");



};

deutsch.onclick = () => {

    setLanguage("deutsch");
    localStorage.setItem("lang", "deutsch");

};

france.onclick = () => {

    setLanguage("france");
    localStorage.setItem("lang", "france");

};

onload = () => {

    setLanguage(localStorage.getItem("lang"));

}


//function setLanguage(getlanguage) {
//    var sPath = window.location.pathname.split('/');
//    var secondLevelLocation = "/" + sPath[1] + "/" + sPath[2];

//    if (getlanguage === "english") {
//        if (window.location.pathname === "/") {

//            title.innerHTML = "Home";
//            contact.innerHTML = "Contact";
//            if (login !== null) {
//                login.innerHTML = "login";
//            }
//            if (register !== null) {
//                register.innerHTML = "Register";
//            }
//            account.innerHTML = "Account";
//            features_items.innerHTML = "Features items";
//            category.innerHTML = "Category";
//            recommended.innerHTML = "recommended items";
//            checkout.innerHTML = "Checkout";
//        }
//        else {
//            if (window.location.pathname === "/Account/login") {
//                title.innerHTML = "Home";
//                contact.innerHTML = "Contact";
//                if (login !== null) {
//                    login.innerHTML = "login";
//                }
//                if (register !== null) {
//                    register.innerHTML = "Register";
//                }
//                account.innerHTML = "Account";
//                checkout.innerHTML = "Checkout";

//            }
//            else {
//                if (secondLevelLocation === "/home/CategoryItems") {
//                    title.innerHTML = "Home";
//                    contact.innerHTML = "Contact";
//                    if (login !== null) {
//                        login.innerHTML = "login";
//                    }
//                    if (register !== null) {
//                        register.innerHTML = "Register";
//                    }
//                    account.innerHTML = "Account";
//                    features_items.innerHTML = "Features items";
//                    category.innerHTML = "Category";

//                    checkout.innerHTML = "Checkout";

//                }
//            }
//        }


//    }
function setLanguage(getlanguage) {
    //var sPath = window.location.pathname.split('/');
    //var secondLevelLocation = "/" + sPath[1] + "/" + sPath[2];

    if (getlanguage === "english") {
        {
            if (title !== null) {
                title.innerHTML = "Home";
            }
            if (contact !== null) {
                contact.innerHTML = "Contact";
            }
            if (login !== null) {
                login.innerHTML = "login";
            }
            if (register !== null) {
                register.innerHTML = "Register";
            }
            if (account !== null) {
                account.innerHTML = "Account";
            }
            if (features_items !== null) {
                features_items.innerHTML = "Features items";
            }
            if (category !== null) {
                category.innerHTML = "Category";
            }
            if (recommended !== null) {
                recommended.innerHTML = "recommended items";
            }
            if (checkout !== null) {
                checkout.innerHTML = "Checkout";
            }
            if (cart !== null) {
                cart.innerHTML = "Cart";
            }
            if (logout !== null) {
                logout.innerHTML = "Logut";
            }
            if (productdetail !== null) {
                productdetail.innerHTML = "Product Details";
            }
            if (productname !== null) {
                productname.innerHTML = "Product Name";
            }
            if (productprice !== null) {
                productprice.innerHTML = "Product Price";
            }
            if (productdesc !== null) {
                productdesc.innerHTML = "Product Description";
            }
        }


    }
    else if (getlanguage === "deutsch") {
        {
            if (title !== null) {
                title.innerHTML = "Home";
            }
            if (contact !== null) {
                contact.innerHTML = "Kontakt";
            }
            if (login !== null) {
                login.innerHTML = "Anmeldung";
            }
            if (register !== null) {
                register.innerHTML = "Registrieren";
            }

            if (account !== null) {
                account.innerHTML = "Konto";
            }
            if (features_items !== null) {
                features_items.innerHTML = "Top-Artikel";
            }
            if (category !== null) {
                category.innerHTML = "Kategorie";
            }
            if (recommended !== null) {
                recommended.innerHTML = "Empfohlene-Artikel";
            }
            if (checkout !== null) {
                checkout.innerHTML = "Prüfen";
            }
            if (cart !== null) {
                cart.innerHTML = "Wagen";
            }
            if (logout !== null) {
                logout.innerHTML = "Auslogen";
            }
            if (productdetail !== null) {
                productdetail.innerHTML = "Produktdetails";
            }
            if (productname !== null) {
                productname.innerHTML = "Produktname";
            }
            if (productprice !== null) {
                productprice.innerHTML = "Produktpreis";
            }
            if (productdesc !== null) {
                productdesc.innerHTML = "Produktbeschreibung";
            }

        }


    }
    else if (getlanguage === "france") {
        {
            if (title !== null) {
                title.innerHTML = "Home";
            }
            if (contact !== null) {
                contact.innerHTML = "Contact";
            }
            if (login !== null) {
                login.innerHTML = "Enregistrement";
            }
            if (register !== null) {
                register.innerHTML = "Enregistrer";
            }

            if (account !== null) {
                account.innerHTML = "Compte";
            }
            if (features_items !== null) {
                features_items.innerHTML = "Top article";
            }
            if (category !== null) {
                category.innerHTML = "Catégorie";
            }
            if (recommended !== null) {
                recommended.innerHTML = "Articles recommandés";
            }
            if (checkout !== null) {
                checkout.innerHTML = "Vérifier";
            }
            if (cart !== null) {
                cart.innerHTML = "Oser";
            }
            if (logout !== null) {
                logout.innerHTML = "Se déconnecter";
            }
            if (productdetail !== null) {
                productdetail.innerHTML = "Détails du produit";
            }
            if (productname !== null) {
                productname.innerHTML = "Nom du produit";
            }
            if (productprice !== null) {
                productprice.innerHTML = "Prix ​​du produit";
            }
            if (productdesc !== null) {
                productdesc.innerHTML = "Description du produit";
            }

        }


    }
}