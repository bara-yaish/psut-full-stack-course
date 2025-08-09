// Exercise #1

const exercise1 = () => {
  let students = [
    { id: 1, name: "Ali", marks: 78, attendance: 85 },
    { id: 2, name: "ٍSara", marks: 92, attendance: 95 },
    { id: 3, name: "Omar", marks: 64, attendance: 70 },
    { id: 4, name: "Lana", marks: 85, attendance: 88 },
    { id: 5, name: "Huda", marks: 47, attendance: 60 },
    { id: 6, name: "Zaid", marks: 59, attendance: 75 },
  ];

  let passingStudents = students.filter((x) => x.marks >= 60);
  console.log(passingStudents);

  students.forEach((x) => {
    x.grade =
      x.marks >= 90
        ? "A"
        : x.marks >= 80
        ? "B"
        : x.marks >= 70
        ? "C"
        : x.marks >= 60
        ? "D"
        : "F";
  });
  console.log(students);

  let newStudents = students.map((x) => ({
    Id: x.id,
    Name: x.name,
    Grade: x.grade,
  }));
  console.log(newStudents);

  let avgAttendance = students.reduce((sum, x) => sum + x.attendance, 0);
  console.log((avgAttendance / students.length).toFixed(2));

  let highPassingStudents = students
    .filter((x) => x.attendance >= 80)
    .map((x) => ({
      Id: x.id,
      Name: x.name,
      Grade: x.grade,
      Attendance: x.attendance,
      Status: x.attendance >= 90 ? "Excellent" : "Good",
    }));
  console.log(highPassingStudents);
};
