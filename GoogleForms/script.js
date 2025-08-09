let x = 10;

let employees = [
  { id: 1, name: "emp1", age: 20, isActive: true },
  { id: 2, name: "emp2", age: 30, isActive: true },
  { id: 3, name: "emp3", age: 29, isActive: true },
  { id: 4, name: "emp4", age: 21, isActive: true },
];

let activeEmployees = employees.filter((x) => x.isActive);

// console.log(activeEmployees);

let numbers = [10, 20, 30, 40];

let sumOfNumbers = numbers.reduce((sum, number) => sum + number, 0);
// console.log(sumOfNumbers);

employees.forEach((emp) => {
  // console.log(`Name: ${emp.name}, Age: ${emp.age}`);
});

employees.forEach((x) => {
  x.salaryIncrease = x.age > 25 ? 0.2 : 0.1;
});

// console.log(employees);

// let userName = window.prompt("Hello, insert your name now");
// window.alert(`Hello, ${userName}!`);

// let num1 = window.prompt("Insert first number", 0);
// let num2 = window.prompt("Insert second number", 0);

// window.alert(Number(num1) + Number(num2));

// for (let i = 1; i <= 6; i++) {
//   document.write(`<h${i}>Hello, World!</h${i}>`);
// }

document.addEventListener("DOMContentLoaded", function () {
  // let element = document.getElementById("email");
  // let element = document.getElementById("description");
  // element.innerHTML = "<span>From Js</span>";
  // element.textContent = "From Js";
  // element.style.backgroundColor = "red";
  // element.value = "byaish@e2abs.com";
  // element.setAttribute("value", "byaish");
  // element.hasAttribute("value");
  // element.hasAttributes();
  // element.removeAttribute("value");
  // console.log(element);
  // let tagElements = document.getElementsByTagName("p");
  // console.log(tagElements);
  // let classElements = document.getElementsByClassName("question");
  // console.log(classElements);
  // let queryIdElement = document.querySelectorAll("#email");
  // let queryClassElement = document.querySelectorAll(".question");
  // let queryTagElement = document.querySelectorAll("p");
  // console.log(queryIdElement);
  // console.log(queryClassElement);
  // console.log(queryTagElement);
  // let newElement = document.createElement("div");
  // newElement.className = "Title";
  // let childElement = document.createElement("span");
  // childElement.textContent = "Hellow from JS";
  // newElement.appendChild(childElement);
  // document.body.appendChild(newElement);

  let submitElement = document.querySelector(".submit");
  submitElement.addEventListener("click", function () {
    let inputElements = document.getElementsByTagName("input");

    Array.from(inputElements).forEach((x) => {
      if (
        x.hasAttribute("required") &&
        (x.value === null || x.value?.trim() === "")
      ) {
        x.classList.add("required-input-warning");
      } else {
        x.classList.remove("required-input-warning");
      }
    });
  });
});
