const backTop = document.querySelector(".back-to-top i")
const actions = document.querySelectorAll(".navbar__link");

window.addEventListener("scroll", function () {
    var y = window.scrollY;
    if (y >= 100) {
        backTop.classList.add("show")
    } else {
        backTop.classList.remove("show");
    }
})
backTop.addEventListener("click", function () {
    window.scroll({
        top: 0,
        behavior: "smooth",
    });
});


actions.forEach((action) => {
    action.addEventListener('click', function () {
        actions.forEach(link => {
            link.classList.remove('navbar__link--active');
        })
        this.classList.add('navbar__link--active');
    })
})