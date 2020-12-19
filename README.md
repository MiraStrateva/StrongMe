# Strong Me

Strong Me is a training system. The idea is to provide an opportunity to have an online personal trainer, receive personal training programs, regular feedback on your progress, advices and easy attachments of your personal training programs and diets. The trainee should provide his measures regularly and should point out his trainings.

The system has 3 types of Users: Administrator, Trainer and Trainee.

When you register in a system you are automatically assigned as a Trainer and you can start your work:
- Add your exercise database. Each exercise should be arranged in some predefined Categories and BodyParts, have a name, description and images;
- Add your template training programs. The idea of the templates is to have some predefined set of exercises which can be used when a personal program is created.
- Invite your trainees. Each trainer is given a unique code which is included in the invitation link. Following the link will help to each trainee to register for his trainer.
- Assign personal training programs to his trainees.
- Follow the progress of his trainees (measurements, trainings) /not implemented yet, the idea is to have some dashboard where trainer will see his trainees, theirs measurements, messages and trainings/
- Write some feedback and advice to his trainees /not implemented
- Adjust the personal trainings of his trainees

The trainee will be able to:
- see all personal training programs assigned from his trainer
- write down his measures
- write down his trainings 

I have also thought for some message exchange functionality or live chat, or both - still not implemented. 

In my project I have used .Net 5 and the template provided by Nikolay Kostov.
- I have added 2 new areas: Instructor and Trainee.
- I have scafolded 2 administration controllers for Category and BodyPart, initial data for both is seeded. Scafolded controllers are modified to use services.
- I have created 7 controllers, 21 views, 3 partial views, 21 unit tests. Many view models, i have 9 models / tables in my database so far. Some more should be added.
- I have used Sandbox to send the invitation for which have added a new Page in Identity area. Some other small changes also are implemented in Identity - code field is added and some layout changes.
- I have wrote a small piece of js code used while template personal trainings are created. 
