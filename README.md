# QuizApp.solution
Alhamdulillah, I have successfully completed my project on an online quiz and testing application in ASP.NET MVC Core. In this application, there are three panels for user roles, namely admin, teacher, and student. Admins have access to manage other admins, teachers, courses, and students. They can also view the results of all quizzes or tests attempted by students.

Teachers have the ability to manage questions in each course, set new quizzes that generate an encrypted room code for each quiz, and provide this code to students for attempts. Teachers can then view the results of all students who attempted that quiz. Students, upon logging in, can see their profile, edit their profiles, and attempt quizzes by entering the room code provided by the teacher.

Every student receives randomly selected questions from a list, ensuring that no two students receive the same set of questions. Additionally, students can attempt each quiz only once. After attempting a quiz, the results can be viewed by both the teacher and admin.

This project follows the clean architecture approach, organized into three projects within one solution. The GUI project manages all user interfaces, the infrastructure project contains all implementations, and the domain project encompasses all database models. I am proud of this achievement and eager to showcase my skills in developing a robust and user-friendly application.




