function getElementsClassName(classname) {
    var classElements = [];
    var elements = document.getElementsByTagName("*");
    for (var i = 0; i < elements.length; i++) {
        if (elements[i].getAttribute("class") == classname) {
            classElements[classElements.length] = elements[i];
        }
    }
    return classElements;
}