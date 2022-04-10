USE rollcall;

DROP TABLE IF EXISTS `Names`;

CREATE TABLE `Names`(
  `val` varchar(128),
  `gender` ENUM('F','M'),
  PRIMARY KEY(`val`,`gender`)
);

DROP TABLE IF EXISTS `Surnames`;

CREATE TABLE `Surnames`(
  `val` varchar(128),
  `gender` ENUM('F','M'),
  PRIMARY KEY(`val`,`gender`)
);

LOCK TABLES `Names` WRITE;

INSERT INTO `Names` (val,gender) values ('Piotr', 'M'), ('Krzysztof', 'M'), ('Andrzej', 'M'), ('Tomasz', 'M'), ('Paweł', 'M'), ('Jan', 'M'), ('Michał', 'M'), ('Marcin', 'M'), ('Jakub', 'M'), ('Adam', 'M'), ('Stanisław', 'M'), ('Marek', 'M'), ('Łukasz', 'M'), ('Grzegorz', 'M'), ('Mateusz', 'M'), ('Wojciech', 'M'), ('Mariusz', 'M'), ('Dariusz', 'M'), ('Zbigniew', 'M'), ('Maciej', 'M'), ('Rafał', 'M'), ('Robert', 'M'), ('Jerzy', 'M'), ('Kamil', 'M'), ('Jacek', 'M'), ('Józef', 'M'), ('Dawid', 'M'), ('Szymon', 'M'), ('Tadeusz', 'M'), ('Ryszard', 'M'), ('Kacper', 'M'), ('Bartosz', 'M'), ('Jarosław', 'M'), ('Janusz', 'M'), ('Sławomir', 'M'), ('Artur', 'M'), ('Mirosław', 'M'), ('Sebastian', 'M'), ('Damian', 'M'), ('Henryk', 'M'), ('Patryk', 'M'), ('Daniel', 'M'), ('Przemysław', 'M'), ('Karol', 'M'), ('Roman', 'M'), ('Kazimierz', 'M'), ('Filip', 'M'), ('Antoni', 'M'), ('Wiesław', 'M'), ('Adrian', 'M'), ('Marian', 'M'), ('Aleksander', 'M'), ('Arkadiusz', 'M'), ('Dominik', 'M'), ('Franciszek', 'M'), ('Bartłomiej', 'M'), ('Mikołaj', 'M'), ('Leszek', 'M'), ('Waldemar', 'M'), ('Krystian', 'M'), ('Wiktor', 'M'), ('Zdzisław', 'M'), ('Radosław', 'M'), ('Bogdan', 'M'), ('Konrad', 'M'), ('Edward', 'M'), ('Mieczysław', 'M'), ('Hubert', 'M'), ('Władysław', 'M'), ('Igor', 'M'), ('Czesław', 'M'), ('Oskar', 'M'), ('Eugeniusz', 'M'), ('Marcel', 'M'), ('Bogusław', 'M'), ('Stefan', 'M'), ('Ireneusz', 'M'), ('Maksymilian', 'M'), ('Zygmunt', 'M'), ('Miłosz', 'M'), ('Witold', 'M'), ('Sylwester', 'M'), ('Oleksandr', 'M'), ('Włodzimierz', 'M'), ('Oliwier', 'M'), ('Alan', 'M'), ('Nikodem', 'M'), ('Zenon', 'M'), ('Leon', 'M'), ('Cezary', 'M'), ('Norbert', 'M'), ('Gabriel', 'M'), ('Julian', 'M'), ('Serhii', 'M'), ('Ignacy', 'M'), ('Błażej', 'M'), ('Andrii', 'M'), ('Fabian', 'M'), ('Tymoteusz', 'M'), ('Volodymyr', 'M'), ('Tymon', 'M'), ('Eryk', 'M'), ('Emil', 'M'), ('Bronisław', 'M'), ('Lech', 'M'), ('Wacław', 'M'), ('Dmytro', 'M'), ('Bolesław', 'M'), ('Ivan', 'M'), ('Mykola', 'M'), ('Ksawery', 'M'), ('Natan', 'M'), ('Vitalii', 'M'), ('Bernard', 'M'), ('Remigiusz', 'M'), ('Ihor', 'M'), ('Vasyl', 'M'), ('Edmund', 'M'), ('Olaf', 'M'), ('Yurii', 'M'), ('Borys', 'M'), ('Maksym', 'M'), ('Kajetan', 'M'), ('Oleh', 'M'), ('Lucjan', 'M'), ('Romuald', 'M'), ('Kuba', 'M'), ('Viktor', 'M'), ('Mykhailo', 'M'), ('Albert', 'M'), ('Tobiasz', 'M'), ('Gracjan', 'M'), ('Szczepan', 'M'), ('Seweryn', 'M'), ('Alfred', 'M'), ('Bruno', 'M'), ('Bohdan', 'M'), ('Ludwik', 'M'), ('Joachim', 'M'), ('Vladyslav', 'M'), ('Lesław', 'M'), ('Ernest', 'M'), ('Kornel', 'M'), ('Bogumił', 'M'), ('Olivier', 'M'), ('Oleksii', 'M'), ('Gerard', 'M'), ('Ruslan', 'M'), ('Alexander', 'M'), ('Jędrzej', 'M'), ('Feliks', 'M'), ('Alex', 'M'), ('Alojzy', 'M'), ('David', 'M'), ('Leonard', 'M'), ('Juliusz', 'M'), ('Pavlo', 'M'), ('Klaudiusz', 'M'), ('Denys', 'M'), ('Anatolii', 'M'), ('Artem', 'M'), ('Vadym', 'M'), ('Aleks', 'M'), ('Dorian', 'M'), ('Yaroslav', 'M'), ('Benedykt', 'M'), ('Petro', 'M'), ('Teodor', 'M'), ('Oliver', 'M'), ('Martin', 'M'), ('Rajmund', 'M'), ('Nataniel', 'M'), ('Yevhen', 'M'), ('Cyprian', 'M'), ('Konstanty', 'M'), ('Denis', 'M'), ('Gustaw', 'M'), ('Valerii', 'M'), ('Michael', 'M'), ('Aliaksandr', 'M'), ('Brajan', 'M'), ('Rudolf', 'M'), ('Yevhenii', 'M'), ('Anton', 'M'), ('Taras', 'M'), ('Samuel', 'M'), ('Mieszko', 'M'), ('Andrei', 'M'), ('Milan', 'M'), ('Hieronim', 'M'), ('Florian', 'M'), ('Marceli', 'M'), ('Wincenty', 'M'), ('Kevin', 'M'), ('Iwo', 'M'), ('Zygfryd', 'M'), ('Viacheslav', 'M'), ('Erwin', 'M'), ('Ariel', 'M'), ('Oleg', 'M'), ('Fryderyk', 'M'), ('Stanislav', 'M'), ('Aleksy', 'M'), ('Beniamin', 'M'), ('Kordian', 'M'), ('Siarhei', 'M'), ('Roland', 'M'), ('Sergiusz', 'M'), ('Kostiantyn', 'M'), ('Jeremi', 'M'), ('Walenty', 'M'), ('Sergii', 'M'), ('Amadeusz', 'M'), ('Olgierd', 'M'), ('Patrick', 'M'), ('Maximilian', 'M'), ('Dzmitry', 'M'), ('Augustyn', 'M'), ('Stepan', 'M'), ('Brunon', 'M'), ('Thomas', 'M'), ('Adolf', 'M'), ('Emilian', 'M'), ('Valentyn', 'M'), ('Kewin', 'M'), ('Victor', 'M'), ('Leopold', 'M'), ('Oscar', 'M'), ('Nicolas', 'M'), ('Nazar', 'M'), ('Oliwer', 'M'), ('Herbert', 'M'), ('Arnold', 'M'), ('Illia', 'M'), ('Albin', 'M'), ('Andriy', 'M'), ('Maurycy', 'M'), ('Alfons', 'M'), ('Eduard', 'M'), ('Wilhelm', 'M'), ('Peter', 'M'), ('Pavel', 'M'), ('Anatol', 'M'), ('Leo', 'M'), ('Mark', 'M'), ('Oktawian', 'M'), ('Helmut', 'M'), ('Leonid', 'M'), ('Jeremiasz', 'M'), ('Walter', 'M'), ('Emanuel', 'M'), ('Manfred', 'M'), ('Korneliusz', 'M'), ('Edwin', 'M'), ('Ginter', 'M'), ('Jonasz', 'M'), ('Nathan', 'M'), ('Christian', 'M'), ('Teofil', 'M'), ('Werner', 'M'), ('Kryspin', 'M'), ('Benjamin', 'M'), ('Jonatan', 'M'), ('Kasper', 'M'), ('Xavier', 'M'), ('Ziemowit', 'M'), ('Iurii', 'M'), ('Jacob', 'M'), ('Rostyslav', 'M'), ('Nikolas', 'M'), ('Walerian', 'M'), ('Max', 'M'), ('Yuriy', 'M'), ('Aliaksei', 'M'), ('Lechosław', 'M'), ('Tytus', 'M'), ('Hugo', 'M'), ('Paul', 'M'), ('Aron', 'M'), ('Miron', 'M'), ('Vitali', 'M'), ('Yury', 'M'), ('Uladzimir', 'M'), ('Patrycjusz', 'M'), ('Danylo', 'M'), ('Ksawier', 'M'), ('Klemens', 'M'), ('Maksim', 'M'), ('Matthew', 'M'), ('Mykyta', 'M'), ('Philip', 'M'), ('Hennadii', 'M'), ('Nazarii', 'M'), ('Yauheni', 'M'), ('Andreas', 'M'), ('Longin', 'M'), ('Ferdynand', 'M'), ('Giorgi', 'M'), ('Kyrylo', 'M'), ('Christopher', 'M'), ('Wawrzyniec', 'M'), ('Horst', 'M'), ('Anthony', 'M'), ('John', 'M'), ('Jacenty', 'M'), ('Mykhaylo', 'M'), ('Mikhail', 'M'), ('Eliasz', 'M'), ('Marco', 'M'), ('Viktar', 'M'), ('Ion', 'M'), ('Ihar', 'M'), ('Aleh', 'M'), ('Kasjan', 'M'), ('Simon', 'M'), ('Maks', 'M'), ('Rafael', 'M'), ('Dionizy', 'M'), ('Roch', 'M'), ('Izydor', 'M'), ('Hryhorii', 'M'), ('Mohamed', 'M'), ('Łucjan', 'M'), ('Jaromir', 'M'), ('Vladimir', 'M'), ('Vitaliy', 'M'), ('Brian', 'M'), ('Ali', 'M'), ('Eligiusz', 'M'), ('Gerhard', 'M'), ('Vincent', 'M'), ('Serhiy', 'M'), ('Jonathan', 'M'), ('Ewald', 'M'), ('Nicholas', 'M'), ('Mikalai', 'M'), ('Nikita', 'M'), ('Yehor', 'M'), ('Antonio', 'M'), ('Kosma', 'M'), ('Richard', 'M'), ('Daniil', 'M'), ('Radomir', 'M'), ('William', 'M'), ('Orest', 'M'), ('Myroslav', 'M'), ('Uladzislau', 'M'), ('Dennis', 'M'), ('Wit', 'M'), ('Zbysław', 'M'), ('Lukas', 'M'), ('Aleksandr', 'M'), ('Bazyli', 'M'), ('Roger', 'M'), ('Valery', 'M'), ('Lucas', 'M'), ('Markus', 'M'), ('Leonardo', 'M'), ('Eric', 'M'), ('Sviatoslav', 'M'), ('Dzianis', 'M'), ('James', 'M'), ('Andrew', 'M'), ('Aaron', 'M'), ('Serghei', 'M'), ('Jordan', 'M'), ('Frank', 'M'), ('Liubomyr', 'M'), ('Mario', 'M'), ('Pascal', 'M'), ('Diego', 'M'), ('Dominic', 'M'), ('Ahmed', 'M'), ('August', 'M'), ('Sergiy', 'M'), ('Arthur', 'M'), ('Erik', 'M'), ('Bonifacy', 'M'), ('Brayan', 'M'), ('Mihail', 'M'), ('Alexandr', 'M'), ('Vadzim', 'M'), ('Raman', 'M'), ('Cyryl', 'M'), ('Klaus', 'M'), ('Zbyszek', 'M'), ('Carlos', 'M'), ('Wolfgang', 'M'), ('Joseph', 'M'), ('Bogusz', 'M'), ('Anatoli', 'M'), ('Allan', 'M'), ('Francesco', 'M'), ('Matteo', 'M'), ('Marko', 'M'), ('Juan', 'M'), ('Luca', 'M'), ('Bryan', 'M'), ('Maxim', 'M'), ('Davyd', 'M'), ('Alessandro', 'M'), ('Alexandru', 'M'), ('Ivo', 'M'), ('Nathaniel', 'M'), ('Donat', 'M'), ('Dymitr', 'M'), ('Jose', 'M'), ('Yan', 'M'), ('Alexandre', 'M'), ('Edgar', 'M'), ('Fedir', 'M'), ('Amir', 'M'), ('Manuel', 'M'), ('Artsiom', 'M'), ('Liam', 'M'), ('Mehmet', 'M'), ('Lubomir', 'M'), ('Luis', 'M'), ('Joshua', 'M'), ('Kurt', 'M'), ('Ilya', 'M'), ('Jakob', 'M'), ('Omar', 'M'), ('Ryan', 'M'), ('Anatoliy', 'M'), ('Gniewomir', 'M'), ('Heronim', 'M'), ('Aureliusz', 'M'), ('Vasile', 'M'), ('Dieter', 'M'), ('Maxymilian', 'M'), ('Ievgen', 'M'), ('Roberto', 'M'), ('Nicolae', 'M'), ('Karim', 'M'), ('Colin', 'M'), ('Hans', 'M'), ('Ian', 'M'), ('Benon', 'M'), ('Felix', 'M'), ('Euzebiusz', 'M'), ('Herman', 'M'), ('Noah', 'M'), ('Justin', 'M'), ('Arsen', 'M'), ('Matthias', 'M'), ('Andrea', 'M'), ('Muhammad', 'M'), ('Mustafa', 'M'), ('Zbyszko', 'M'), ('Mohammad', 'M'), ('Philipp', 'M'), ('Kai', 'M'), ('Tymofii', 'M'), ('Iaroslav', 'M'), ('Vadim', 'M'), ('Davit', 'M'), ('Ricardo', 'M'), ('George', 'M'), ('Cristian', 'M'), ('Henry', 'M'), ('Armin', 'M'), ('Chrystian', 'M'), ('Vasili', 'M'), ('Tristan', 'M'), ('Zachariasz', 'M'), ('Vladislav', 'M'), ('Kiryl', 'M'), ('Heorhii', 'M'), ('Bartek', 'M'), ('Pablo', 'M'), ('Sergiu', 'M'), ('Angelo', 'M'), ('Mohammed', 'M'), ('Matvii', 'M'), ('Viachaslau', 'M'), ('Tymur', 'M'), ('Mikita', 'M'), ('Gheorghe', 'M'), ('Hilary', 'M'), ('Ronald', 'M'), ('Zachary', 'M'), ('Giuseppe', 'M'), ('Michal', 'M'), ('Dumitru', 'M'), ('Kristian', 'M'), ('Sergey', 'M'), ('Harald', 'M'), ('Jorge', 'M'), ('Narcyz', 'M'), ('January', 'M'), ('Reinhold', 'M'), ('Tobias', 'M'), ('Dylan', 'M'), ('Tom', 'M'), ('Alejandro', 'M'), ('Rajnold', 'M'), ('Samir', 'M'), ('Ibrahim', 'M'), ('Giovanni', 'M');
INSERT INTO `Names` (val,gender) values ('Anna', 'F'), ('Maria', 'F'), ('Katarzyna', 'F'), ('Małgorzata', 'F'), ('Agnieszka', 'F'), ('Barbara', 'F'), ('Ewa', 'F'), ('Krystyna', 'F'), ('Magdalena', 'F'), ('Elżbieta', 'F'), ('Joanna', 'F'), ('Aleksandra', 'F'), ('Zofia', 'F'), ('Monika', 'F'), ('Teresa', 'F'), ('Danuta', 'F'), ('Natalia', 'F'), ('Julia', 'F'), ('Karolina', 'F'), ('Marta', 'F'), ('Beata', 'F'), ('Dorota', 'F'), ('Halina', 'F'), ('Jadwiga', 'F'), ('Janina', 'F'), ('Alicja', 'F'), ('Jolanta', 'F'), ('Grażyna', 'F'), ('Iwona', 'F'), ('Irena', 'F'), ('Paulina', 'F'), ('Justyna', 'F'), ('Zuzanna', 'F'), ('Bożena', 'F'), ('Wiktoria', 'F'), ('Urszula', 'F'), ('Renata', 'F'), ('Hanna', 'F'), ('Sylwia', 'F'), ('Agata', 'F'), ('Helena', 'F'), ('Patrycja', 'F'), ('Maja', 'F'), ('Izabela', 'F'), ('Emilia', 'F'), ('Aneta', 'F'), ('Weronika', 'F'), ('Ewelina', 'F'), ('Oliwia', 'F'), ('Martyna', 'F'), ('Klaudia', 'F'), ('Marianna', 'F'), ('Marzena', 'F'), ('Gabriela', 'F'), ('Stanisława', 'F'), ('Dominika', 'F'), ('Kinga', 'F'), ('Lena', 'F'), ('Edyta', 'F'), ('Amelia', 'F'), ('Wiesława', 'F'), ('Kamila', 'F'), ('Wanda', 'F'), ('Alina', 'F'), ('Lidia', 'F'), ('Lucyna', 'F'), ('Mariola', 'F'), ('Nikola', 'F'), ('Mirosława', 'F'), ('Wioletta', 'F'), ('Milena', 'F'), ('Daria', 'F'), ('Angelika', 'F'), ('Kazimiera', 'F'), ('Genowefa', 'F'), ('Bogumiła', 'F'), ('Antonina', 'F'), ('Laura', 'F'), ('Olga', 'F'), ('Sandra', 'F'), ('Henryka', 'F'), ('Ilona', 'F'), ('Józefa', 'F'), ('Stefania', 'F'), ('Michalina', 'F'), ('Sabina', 'F'), ('Bogusława', 'F'), ('Marlena', 'F'), ('Regina', 'F'), ('Nadia', 'F'), ('Łucja', 'F'), ('Anita', 'F'), ('Kornelia', 'F'), ('Władysława', 'F'), ('Czesława', 'F'), ('Aniela', 'F'), ('Iga', 'F'), ('Liliana', 'F'), ('Jagoda', 'F'), ('Marcelina', 'F'), ('Nina', 'F'), ('Pola', 'F'), ('Wioleta', 'F'), ('Adrianna', 'F'), ('Roksana', 'F'), ('Karina', 'F'), ('Dagmara', 'F'), ('Cecylia', 'F'), ('Malwina', 'F'), ('Sara', 'F'), ('Leokadia', 'F'), ('Zdzisława', 'F'), ('Żaneta', 'F'), ('Eliza', 'F'), ('Bronisława', 'F'), ('Eugenia', 'F'), ('Róża', 'F'), ('Tetiana', 'F'), ('Bernadeta', 'F'), ('Nataliia', 'F'), ('Kaja', 'F'), ('Olena', 'F'), ('Julita', 'F'), ('Daniela', 'F'), ('Aldona', 'F'), ('Iryna', 'F'), ('Anastazja', 'F'), ('Klara', 'F'), ('Blanka', 'F'), ('Oksana', 'F'), ('Rozalia', 'F'), ('Violetta', 'F'), ('Magda', 'F'), ('Celina', 'F'), ('Diana', 'F'), ('Svitlana', 'F'), ('Honorata', 'F'), ('Lilianna', 'F'), ('Olha', 'F'), ('Adriana', 'F'), ('Paula', 'F'), ('Matylda', 'F'), ('Brygida', 'F'), ('Gertruda', 'F'), ('Mieczysława', 'F'), ('Izabella', 'F'), ('Mariia', 'F'), ('Bożenna', 'F'), ('Aurelia', 'F'), ('Yuliia', 'F'), ('Kalina', 'F'), ('Marika', 'F'), ('Elwira', 'F'), ('Marzanna', 'F'), ('Sonia', 'F'), ('Andżelika', 'F'), ('Nela', 'F'), ('Viktoriia', 'F'), ('Arleta', 'F'), ('Liudmyla', 'F'), ('Kateryna', 'F'), ('Anastasiia', 'F'), ('Olivia', 'F'), ('Franciszka', 'F'), ('Adela', 'F'), ('Luiza', 'F'), ('Judyta', 'F'), ('Halyna', 'F'), ('Alfreda', 'F'), ('Nicole', 'F'), ('Natasza', 'F'), ('Tatiana', 'F'), ('Nicola', 'F'), ('Jowita', 'F'), ('Victoria', 'F'), ('Maryna', 'F'), ('Romana', 'F'), ('Apolonia', 'F'), ('Vanessa', 'F'), ('Ludwika', 'F'), ('Julianna', 'F'), ('Tamara', 'F'), ('Eleonora', 'F'), ('Valentyna', 'F'), ('Marzenna', 'F'), ('Wacława', 'F'), ('Jessica', 'F'), ('Inga', 'F'), ('Liwia', 'F'), ('Zenona', 'F'), ('Estera', 'F'), ('Melania', 'F'), ('Ada', 'F'), ('Nadiia', 'F'), ('Bernadetta', 'F'), ('Mia', 'F'), ('Walentyna', 'F'), ('Hildegarda', 'F'), ('Inna', 'F'), ('Zenobia', 'F'), ('Donata', 'F'), ('Ludmiła', 'F'), ('Felicja', 'F'), ('Anetta', 'F'), ('Bianka', 'F'), ('Larysa', 'F'), ('Romualda', 'F'), ('Otylia', 'F'), ('Elena', 'F'), ('Gizela', 'F'), ('Amanda', 'F'), ('Pelagia', 'F'), ('Irmina', 'F'), ('Liubov', 'F'), ('Rita', 'F'), ('Eryka', 'F'), ('Oleksandra', 'F'), ('Ida', 'F'), ('Lilia', 'F'), ('Sławomira', 'F'), ('Maya', 'F'), ('Bernarda', 'F'), ('Lilla', 'F'), ('Yana', 'F'), ('Longina', 'F'), ('Alla', 'F'), ('Waleria', 'F'), ('Teodozja', 'F'), ('Ryszarda', 'F'), ('Teodora', 'F'), ('Emma', 'F'), ('Adelajda', 'F'), ('Kamilla', 'F'), ('Mirela', 'F'), ('Albina', 'F'), ('Liliia', 'F'), ('Faustyna', 'F'), ('Alona', 'F'), ('Samanta', 'F'), ('Olimpia', 'F'), ('Lila', 'F'), ('Feliksa', 'F'), ('Alexandra', 'F'), ('Lucja', 'F'), ('Maryla', 'F'), ('Marcela', 'F'), ('Sofiia', 'F'), ('Zyta', 'F'), ('Sofia', 'F'), ('Leonarda', 'F'), ('Gaja', 'F'), ('Mariana', 'F'), ('Angelina', 'F'), ('Ivanna', 'F'), ('Veronika', 'F'), ('Viktoria', 'F'), ('Oktawia', 'F'), ('Ksenia', 'F'), ('Valeriia', 'F'), ('Konstancja', 'F'), ('Pamela', 'F'), ('Zoja', 'F'), ('Khrystyna', 'F'), ('Wanessa', 'F'), ('Bolesława', 'F'), ('Bogna', 'F'), ('Jagna', 'F'), ('Mirella', 'F'), ('Claudia', 'F'), ('Lesia', 'F'), ('Noemi', 'F'), ('Krzysztofa', 'F'), ('Salomea', 'F'), ('Vira', 'F'), ('Nadzieja', 'F'), ('Edwarda', 'F'), ('Elfryda', 'F'), ('Nel', 'F'), ('Inez', 'F'), ('Mariya', 'F'), ('Sophie', 'F'), ('Alena', 'F'), ('Emily', 'F'), ('Ola', 'F'), ('Edeltrauda', 'F'), ('Stella', 'F'), ('Michelle', 'F'), ('Nataliya', 'F'), ('Yevheniia', 'F'), ('Sarah', 'F'), ('Angela', 'F'), ('Tola', 'F'), ('Marina', 'F'), ('Yelyzaveta', 'F'), ('Yuliya', 'F'), ('Miriam', 'F'), ('Ines', 'F'), ('Olesia', 'F'), ('Ruslana', 'F'), ('Irina', 'F'), ('Lea', 'F'), ('Iza', 'F'), ('Arletta', 'F'), ('Jaśmina', 'F'), ('Wiera', 'F'), ('Galyna', 'F'), ('Roma', 'F'), ('Ruta', 'F'), ('Natalie', 'F'), ('Marietta', 'F'), ('Klementyna', 'F'), ('Manuela', 'F'), ('Klaudyna', 'F'), ('Helga', 'F'), ('Antonia', 'F'), ('Anastasiya', 'F'), ('Vita', 'F'), ('Polina', 'F'), ('Eva', 'F'), ('Raisa', 'F'), ('Tatsiana', 'F'), ('Gerda', 'F'), ('Greta', 'F'), ('Lidiia', 'F'), ('Natallia', 'F'), ('Karyna', 'F'), ('Marharyta', 'F'), ('Ana', 'F'), ('Volha', 'F'), ('Myroslava', 'F'), ('Irmgarda', 'F'), ('Teofila', 'F'), ('Kristina', 'F'), ('Mila', 'F'), ('Eulalia', 'F'), ('Florentyna', 'F'), ('Dobrosława', 'F'), ('Bernardyna', 'F'), ('Uliana', 'F'), ('Andrea', 'F'), ('Jarosława', 'F'), ('Zhanna', 'F'), ('Martina', 'F'), ('Sophia', 'F'), ('Katsiaryna', 'F'), ('Erna', 'F'), ('Anzhela', 'F'), ('Tetyana', 'F'), ('Iuliia', 'F'), ('Erika', 'F'), ('Veronica', 'F'), ('Lara', 'F'), ('Nicol', 'F'), ('Livia', 'F'), ('Daryna', 'F'), ('Wanesa', 'F'), ('Hanka', 'F'), ('Linda', 'F'), ('Luba', 'F'), ('Anastasia', 'F'), ('Dżesika', 'F'), ('Marcjanna', 'F'), ('Jennifer', 'F'), ('Ala', 'F'), ('Malina', 'F'), ('Anhelina', 'F'), ('Nikol', 'F'), ('Ganna', 'F'), ('Majka', 'F'), ('Wirginia', 'F'), ('Patricia', 'F'), ('Kseniia', 'F'), ('Zinaida', 'F'), ('Isabella', 'F'), ('Marita', 'F'), ('Gloria', 'F'), ('Balbina', 'F'), ('Kunegunda', 'F'), ('Galina', 'F'), ('Simona', 'F'), ('Alisa', 'F'), ('Seweryna', 'F'), ('Dajana', 'F'), ('Anika', 'F'), ('Jana', 'F'), ('Filomena', 'F'), ('Emanuela', 'F'), ('Sabrina', 'F'), ('Mira', 'F'), ('Hannah', 'F'), ('Svetlana', 'F'), ('Berenika', 'F'), ('Carmen', 'F'), ('Caroline', 'F'), ('Kornela', 'F'), ('Gabriella', 'F'), ('Nikoletta', 'F'), ('Dariia', 'F'), ('Darya', 'F'), ('Ramona', 'F'), ('Blandyna', 'F'), ('Benedykta', 'F'), ('Iwonna', 'F'), ('Leontyna', 'F'), ('Lubomira', 'F'), ('Sviatlana', 'F'), ('Bogdana', 'F'), ('Żaklina', 'F'), ('Ligia', 'F'), ('Maryia', 'F'), ('Amalia', 'F'), ('Kira', 'F'), ('Nelly', 'F'), ('Liudmila', 'F'), ('Dalia', 'F'), ('Gabryela', 'F'), ('Alice', 'F'), ('Valentina', 'F'), ('Naomi', 'F'), ('Zoriana', 'F'), ('Emila', 'F'), ('Lily', 'F'), ('Milana', 'F'), ('Elizabeth', 'F'), ('Ina', 'F'), ('Nadiya', 'F'), ('Bohdana', 'F'), ('Ingrid', 'F'), ('Alicia', 'F'), ('Ingeborga', 'F'), ('Michaela', 'F'), ('Swietłana', 'F'), ('Sybilla', 'F'), ('Irma', 'F'), ('Idalia', 'F'), ('Lyudmyla', 'F'), ('Nelia', 'F'), ('Bianca', 'F'), ('Domicela', 'F'), ('Celestyna', 'F'), ('Snizhana', 'F'), ('Tekla', 'F'), ('Gracjana', 'F'), ('Inka', 'F'), ('Józefina', 'F'), ('Viktoryia', 'F'), ('Hana', 'F'), ('Lilly', 'F'), ('Roxana', 'F'), ('Yaroslava', 'F'), ('Violeta', 'F'), ('Margarita', 'F'), ('Kaya', 'F'), ('Vladyslava', 'F'), ('Ella', 'F'), ('Sława', 'F'), ('Leonia', 'F'), ('Bernardeta', 'F'), ('Żanetta', 'F'), ('Lyubov', 'F'), ('Margareta', 'F'), ('Carolina', 'F'), ('Anzhelika', 'F'), ('Marie', 'F'), ('Abigail', 'F'), ('Ariana', 'F'), ('Benita', 'F'), ('Berta', 'F'), ('Monica', 'F'), ('Ekaterina', 'F'), ('Lina', 'F'), ('Dobrawa', 'F'), ('Ela', 'F'), ('Jesika', 'F'), ('Kasandra', 'F'), ('Zoia', 'F'), ('Karin', 'F'), ('Eunika', 'F'), ('Dobrochna', 'F'), ('Cristina', 'F'), ('Dagna', 'F'), ('Jessika', 'F'), ('Oriana', 'F'), ('Viktoriya', 'F'), ('Melisa', 'F'), ('Yeva', 'F'), ('Melissa', 'F'), ('Zlata', 'F'), ('Zoe', 'F'), ('Aliaksandra', 'F'), ('Arina', 'F'), ('Augustyna', 'F'), ('Nathalie', 'F'), ('Fatima', 'F'), ('Lesława', 'F'), ('Lisa', 'F'), ('Vasylyna', 'F'), ('Giulia', 'F'), ('Solomiia', 'F');

UNLOCK TABLES;

LOCK TABLES `Surnames` WRITE;

INSERT INTO `Surnames` (val,gender) values ('Nowak','M'), ('Kowalski','M'), ('Wiśniewski','M'), ('Wójcik','M'), ('Kowalczyk','M'), ('Kamiński','M'), ('Lewandowski','M'), ('Zieliński','M'), ('Szymański','M'), ('Woźniak','M'), ('Dąbrowski','M'), ('Kozłowski','M'), ('Mazur','M'), ('Jankowski','M'), ('Kwiatkowski','M'), ('Wojciechowski','M'), ('Krawczyk','M'), ('Kaczmarek','M'), ('Piotrowski','M'), ('Grabowski','M'), ('Zając','M'), ('Pawłowski','M'), ('Król','M'), ('Michalski','M'), ('Wróbel','M'), ('Wieczorek','M'), ('Jabłoński','M'), ('Nowakowski','M'), ('Majewski','M'), ('Olszewski','M'), ('Dudek','M'), ('Stępień','M'), ('Jaworski','M'), ('Adamczyk','M'), ('Malinowski','M'), ('Górski','M'), ('Pawlak','M'), ('Nowicki','M'), ('Sikora','M'), ('Witkowski','M'), ('Rutkowski','M'), ('Walczak','M'), ('Baran','M'), ('Michalak','M'), ('Szewczyk','M'), ('Ostrowski','M'), ('Tomaszewski','M'), ('Zalewski','M'), ('Wróblewski','M'), ('Pietrzak','M'), ('Jasiński','M'), ('Marciniak','M'), ('Sadowski','M'), ('Bąk','M'), ('Zawadzki','M'), ('Duda','M'), ('Jakubowski','M'), ('Wilk','M'), ('Chmielewski','M'), ('Borkowski','M'), ('Włodarczyk','M'), ('Sokołowski','M'), ('Szczepański','M'), ('Sawicki','M'), ('Lis','M'), ('Kucharski','M'), ('Kalinowski','M'), ('Wysocki','M'), ('Mazurek','M'), ('Kubiak','M'), ('Maciejewski','M'), ('Kołodziej','M'), ('Kaźmierczak','M'), ('Czarnecki','M'), ('Sobczak','M'), ('Konieczny','M'), ('Krupa','M'), ('Głowacki','M'), ('Urbański','M'), ('Mróz','M'), ('Wasilewski','M'), ('Zakrzewski','M'), ('Krajewski','M'), ('Laskowski','M'), ('Sikorski','M'), ('Ziółkowski','M'), ('Gajewski','M'), ('Szulc','M'), ('Makowski','M'), ('Kaczmarczyk','M'), ('Brzeziński','M'), ('Baranowski','M'), ('Kozak','M'), ('Przybylski','M'), ('Szymczak','M'), ('Kania','M'), ('Janik','M'), ('Błaszczyk','M'), ('Borowski','M'), ('Adamski','M'), ('Górecki','M'), ('Szczepaniak','M'), ('Chojnacki','M'), ('Kozioł','M'), ('Leszczyński','M'), ('Mucha','M'), ('Lipiński','M'), ('Czerwiński','M'), ('Kowalewski','M'), ('Andrzejewski','M'), ('Wesołowski','M'), ('Mikołajczyk','M'), ('Zięba','M'), ('Cieślak','M'), ('Jarosz','M'), ('Musiał','M'), ('Kowalik','M'), ('Markowski','M'), ('Kołodziejczyk','M'), ('Kopeć','M'), ('Brzozowski','M'), ('Nowacki','M'), ('Piątek','M'), ('Żak','M'), ('Domański','M'), ('Orłowski','M'), ('Pawlik','M'), ('Kurek','M'), ('Ciesielski','M'), ('Tomczyk','M'), ('Tomczak','M'), ('Wójtowicz','M'), ('Wawrzyniak','M'), ('Kot','M'), ('Polak','M'), ('Kruk','M'), ('Wolski','M'), ('Markiewicz','M'), ('Sowa','M'), ('Stasiak','M'), ('Jastrzębski','M'), ('Stankiewicz','M'), ('Karpiński','M'), ('Urbaniak','M'), ('Klimek','M'), ('Łuczak','M'), ('Piasecki','M'), ('Czajkowski','M'), ('Wierzbicki','M'), ('Nawrocki','M'), ('Gajda','M'), ('Bednarek','M'), ('Bielecki','M'), ('Dziedzic','M'), ('Stefański','M'), ('Madej','M'), ('Janicki','M'), ('Milewski','M'), ('Sosnowski','M'), ('Skiba','M'), ('Kowal','M'), ('Leśniak','M'), ('Majchrzak','M'), ('Maj','M'), ('Jóźwiak','M'), ('Urban','M'), ('Śliwiński','M'), ('Małecki','M'), ('Socha','M'), ('Marek','M'), ('Domagała','M'), ('Bednarczyk','M'), ('Kasprzak','M'), ('Wrona','M'), ('Dobrowolski','M'), ('Pająk','M'), ('Matuszewski','M'), ('Michalik','M'), ('Ratajczak','M'), ('Olejniczak','M'), ('Orzechowski','M'), ('Wilczyński','M'), ('Romanowski','M'), ('Świątek','M'), ('Kurowski','M'), ('Olejnik','M'), ('Grzelak','M'), ('Łukasik','M'), ('Rogowski','M'), ('Owczarek','M'), ('Mazurkiewicz','M'), ('Bukowski','M'), ('Sroka','M'), ('Sobolewski','M'), ('Barański','M'), ('Kosiński','M'), ('Kędzierski','M'), ('Rybak','M'), ('Marszałek','M'), ('Zych','M'), ('Bednarz','M'), ('Sobczyk','M'), ('Skowroński','M'), ('Matusiak','M'), ('Marcinkowski','M'), ('Lisowski','M'), ('Chrzanowski','M'), ('Kozieł','M'), ('Świderski','M'), ('Kasprzyk','M'), ('Bednarski','M'), ('Białek','M'), ('Pluta','M'), ('Witek','M'), ('Kwiecień','M'), ('Kuczyński','M'), ('Grzybowski','M'), ('Paluch','M'), ('Janiszewski','M'), ('Turek','M'), ('Chmiel','M'), ('Muszyński','M'), ('Czajka','M'), ('Jędrzejewski','M'), ('Morawski','M'), ('Marczak','M'), ('Małek','M'), ('Marzec','M'), ('Kaczor','M'), ('Śliwa','M'), ('Żukowski','M'), ('Kubicki','M'), ('Czaja','M'), ('Piekarski','M'), ('Czech','M'), ('Szczęsny','M'), ('Osiński','M'), ('Przybysz','M'), ('Krzemiński','M'), ('Kulesza','M'), ('Janowski','M'), ('Stefaniak','M'), ('Gołębiewski','M'), ('Biernacki','M'), ('Lech','M'), ('Rak','M'), ('Smoliński','M'), ('Szydłowski','M'), ('Staniszewski','M'), ('Lewicki','M'), ('Serafin','M'), ('Banach','M'), ('Kujawa','M'), ('Michałowski','M'), ('Cieślik','M'), ('Góra','M'), ('Kacprzak','M'), ('Murawski','M'), ('Popławski','M'), ('Stachowiak','M'), ('Pietrzyk','M'), ('Dębski','M'), ('Rudnicki','M'), ('Piątkowski','M'), ('Żurek','M'), ('Górny','M'), ('Górka','M'), ('Banaś','M'), ('Zawada','M'), ('Niemiec','M'), ('Karczewski','M'), ('Podgórski','M'), ('Matysiak','M'), ('Żurawski','M'), ('Sowiński','M'), ('Klimczak','M'), ('Czyż','M'), ('Gołębiowski','M'), ('Rosiński','M'), ('Bieniek','M'), ('Kuś','M'), ('Drozd','M'), ('Gruszka','M'), ('Godlewski','M'), ('Skrzypczak','M'), ('Augustyniak','M'), ('Krawiec','M'), ('Grochowski','M'), ('Panek','M'), ('Ptak','M'), ('Przybyła','M'), ('Gil','M'), ('Komorowski','M'), ('Winiarski','M'), ('Różański','M'), ('Konopka','M'), ('Siwek','M'), ('Słowik','M'), ('Cybulski','M'), ('Kulik','M'), ('Leśniewski','M'), ('Grzyb','M'), ('Szczepanik','M'), ('Kłos','M'), ('Krzyżanowski','M'), ('Cichoń','M'), ('Zarzycki','M'), ('Zaremba','M'), ('Graczyk','M'), ('Tokarski','M'), ('Mielczarek','M'), ('Kaczyński','M'), ('Młynarczyk','M'), ('Mikołajczak','M'), ('Stańczyk','M'), ('Strzelecki','M'), ('Cichocki','M'), ('Szostak','M'), ('Szymczyk','M'), ('Buczek','M'), ('Biernat','M'), ('Węgrzyn','M'), ('Jurek','M'), ('Maćkowiak','M'), ('Szczygieł','M'), ('Skowron','M'), ('Gąsior','M'), ('Bartkowiak','M'), ('Janus','M'), ('Bogusz','M'), ('Cieśla','M'), ('Filipiak','M'), ('Gawron','M'), ('Kaleta','M'), ('Janiak','M'), ('Niewiadomski','M'), ('Kucharczyk','M'), ('Rzepka','M'), ('Kula','M'), ('Kostrzewa','M'), ('Kubik','M'), ('Pałka','M'), ('Książek','M'), ('Rakowski','M'), ('Bagiński','M'), ('Sienkiewicz','M'), ('Gawlik','M'), ('Różycki','M'), ('Antczak','M'), ('Frączek','M'), ('Bartczak','M'), ('Malec','M'), ('Zaręba','M'), ('Banasiak','M'), ('Trzciński','M'), ('Żebrowski','M'), ('Królikowski','M'), ('Rogalski','M'), ('Długosz','M'), ('Hajduk','M'), ('Mikulski','M'), ('Rogala','M'), ('Rojek','M'), ('Lach','M'), ('Borek','M'), ('Tkaczyk','M'), ('Kisiel','M'), ('Grzegorczyk','M'), ('Trojanowski','M'), ('Gawroński','M'), ('Stec','M'), ('Witczak','M'), ('Dobosz','M'), ('Maliszewski','M'), ('Wąsik','M'), ('Wolny','M'), ('Rybicki','M'), ('Radomski','M'), ('Ślusarczyk','M'), ('Mika','M'), ('Kmiecik','M'), ('Białas','M'), ('Zaborowski','M'), ('Pawelec','M'), ('Gałązka','M'), ('Walkowiak','M'), ('Grzesiak','M'), ('Kaczorowski','M'), ('Bogucki','M'), ('Lipski','M'), ('Sokół','M'), ('Frankowski','M'), ('Wnuk','M'), ('Frąckowiak','M'), ('Bochenek','M'), ('Cichy','M'), ('Karwowski','M'), ('Zygmunt','M'), ('Kwaśniewski','M'), ('Nawrot','M'), ('Wójcicki','M'), ('Żmuda','M'), ('Więckowski','M'), ('Juszczak','M'), ('Dudziński','M'), ('Janas','M'), ('Pietras','M'), ('Gaweł','M'), ('Mroczek','M'), ('Pasternak','M'), ('Skrzypek','M'), ('Lasota','M'), ('Rosiak','M'), ('Wojtczak','M'), ('Łapiński','M'), ('Kołodziejski','M'), ('Jędrzejczyk','M'), ('Misiak','M'), ('Wroński','M'), ('Karaś','M'), ('Piórkowski','M'), ('Krysiak','M'), ('Burzyński','M'), ('Bujak','M'), ('Fijałkowski','M'), ('Dąbek','M'), ('Czyżewski','M'), ('Gruszczyński','M'), ('Kubacki','M'), ('Piwowarczyk','M'), ('Gutowski','M'), ('Masłowski','M'), ('Sołtys','M'), ('Borowiec','M'), ('Szafrański','M'), ('Jagodziński','M'), ('Łukaszewski','M'), ('Stelmach','M'), ('Kałużny','M'), ('Zajączkowski','M'), ('Tarnowski','M'), ('Jagiełło','M'), ('Zielonka','M'), ('Cebula','M'), ('Łukasiewicz','M'), ('Lisiecki','M'), ('Bielawski','M'), ('Kałuża','M'), ('Woźny','M'), ('Cygan','M'), ('Pilarski','M'), ('Krakowiak','M'), ('Jurkiewicz','M'), ('Skibiński','M'), ('Skoczylas','M'), ('Drzewiecki','M'), ('Szcześniak','M'), ('Wilczek','M'), ('Falkowski','M'), ('Pakuła','M'), ('Bober','M'), ('Filipek','M'), ('Jędrzejczak','M'), ('Kędziora','M'), ('Sobieraj','M'), ('Jakubiak','M'), ('Gąsiorowski','M'), ('Strzelczyk','M'), ('Dębowski','M'), ('Twardowski','M'), ('Flis','M'), ('Raczyński','M'), ('Kubica','M'), ('Gołąb','M'), ('Wasiak','M'), ('Mierzejewski','M'), ('Stanek','M'), ('Guzik','M'), ('Górniak','M'), ('Góral','M'), ('Więcek','M'), ('Bartosik','M'), ('Kulig','M'), ('Majcher','M'), ('Wolak','M'), ('Matuszak','M'), ('Motyka','M'), ('Drozdowski','M'), ('Grzywacz','M'), ('Szwed','M'), ('Bilski','M'), ('Rusin','M'), ('Nowaczyk','M'), ('Cholewa','M'), ('Czapla','M'), ('Bury','M'), ('Dziuba','M'), ('Wojtas','M'), ('Stolarczyk','M'), ('Florek','M');
INSERT INTO `Surnames` (val,gender) values ('Nowak', 'F'), ('Kowalska', 'F'), ('Wiśniewska', 'F'), ('Wójcik', 'F'), ('Kowalczyk', 'F'), ('Kamińska', 'F'), ('Lewandowska', 'F'), ('Zielińska', 'F'), ('Szymańska', 'F'), ('Dąbrowska', 'F'), ('Woźniak', 'F'), ('Kozłowska', 'F'), ('Jankowska', 'F'), ('Mazur', 'F'), ('Kwiatkowska', 'F'), ('Wojciechowska', 'F'), ('Krawczyk', 'F'), ('Kaczmarek', 'F'), ('Piotrowska', 'F'), ('Grabowska', 'F'), ('Pawłowska', 'F'), ('Michalska', 'F'), ('Zając', 'F'), ('Król', 'F'), ('Wieczorek', 'F'), ('Jabłońska', 'F'), ('Wróbel', 'F'), ('Nowakowska', 'F'), ('Majewska', 'F'), ('Olszewska', 'F'), ('Adamczyk', 'F'), ('Jaworska', 'F'), ('Malinowska', 'F'), ('Stępień', 'F'), ('Dudek', 'F'), ('Górska', 'F'), ('Nowicka', 'F'), ('Pawlak', 'F'), ('Witkowska', 'F'), ('Sikora', 'F'), ('Walczak', 'F'), ('Rutkowska', 'F'), ('Michalak', 'F'), ('Szewczyk', 'F'), ('Ostrowska', 'F'), ('Baran', 'F'), ('Tomaszewska', 'F'), ('Pietrzak', 'F'), ('Zalewska', 'F'), ('Wróblewska', 'F'), ('Marciniak', 'F'), ('Jasińska', 'F'), ('Jakubowska', 'F'), ('Sadowska', 'F'), ('Zawadzka', 'F'), ('Duda', 'F'), ('Bąk', 'F'), ('Włodarczyk', 'F'), ('Borkowska', 'F'), ('Chmielewska', 'F'), ('Wilk', 'F'), ('Sokołowska', 'F'), ('Szczepańska', 'F'), ('Sawicka', 'F'), ('Lis', 'F'), ('Kucharska', 'F'), ('Maciejewska', 'F'), ('Kalinowska', 'F'), ('Mazurek', 'F'), ('Wysocka', 'F'), ('Kubiak', 'F'), ('Kołodziej', 'F'), ('Czarnecka', 'F'), ('Kaźmierczak', 'F'), ('Urbańska', 'F'), ('Sobczak', 'F'), ('Głowacka', 'F'), ('Krajewska', 'F'), ('Krupa', 'F'), ('Zakrzewska', 'F'), ('Sikorska', 'F'), ('Ziółkowska', 'F'), ('Wasilewska', 'F'), ('Laskowska', 'F'), ('Gajewska', 'F'), ('Mróz', 'F'), ('Makowska', 'F'), ('Szulc', 'F'), ('Brzezińska', 'F'), ('Przybylska', 'F'), ('Baranowska', 'F'), ('Kaczmarczyk', 'F'), ('Szymczak', 'F'), ('Adamska', 'F'), ('Błaszczyk', 'F'), ('Borowska', 'F'), ('Górecka', 'F'), ('Kania', 'F'), ('Szczepaniak', 'F'), ('Janik', 'F'), ('Leszczyńska', 'F'), ('Czerwińska', 'F'), ('Chojnacka', 'F'), ('Lipińska', 'F'), ('Kowalewska', 'F'), ('Wesołowska', 'F'), ('Kozak', 'F'), ('Mikołajczyk', 'F'), ('Andrzejewska', 'F'), ('Mucha', 'F'), ('Jarosz', 'F'), ('Cieślak', 'F'), ('Konieczna', 'F'), ('Kozioł', 'F'), ('Zięba', 'F'), ('Markowska', 'F'), ('Kowalik', 'F'), ('Kołodziejczyk', 'F'), ('Musiał', 'F'), ('Brzozowska', 'F'), ('Domańska', 'F'), ('Pawlik', 'F'), ('Tomczyk', 'F'), ('Orłowska', 'F'), ('Piątek', 'F'), ('Nowacka', 'F'), ('Kopeć', 'F'), ('Tomczak', 'F'), ('Ciesielska', 'F'), ('Żak', 'F'), ('Kurek', 'F'), ('Wawrzyniak', 'F'), ('Markiewicz', 'F'), ('Wójtowicz', 'F'), ('Wolska', 'F'), ('Polak', 'F'), ('Kruk', 'F'), ('Kot', 'F'), ('Stankiewicz', 'F'), ('Jastrzębska', 'F'), ('Sowa', 'F'), ('Urbaniak', 'F'), ('Karpińska', 'F'), ('Łuczak', 'F'), ('Stasiak', 'F'), ('Czajkowska', 'F'), ('Wierzbicka', 'F'), ('Nawrocka', 'F'), ('Piasecka', 'F'), ('Klimek', 'F'), ('Dziedzic', 'F'), ('Sosnowska', 'F'), ('Bednarek', 'F'), ('Janicka', 'F'), ('Stefańska', 'F'), ('Bielecka', 'F'), ('Gajda', 'F'), ('Milewska', 'F'), ('Madej', 'F'), ('Majchrzak', 'F'), ('Jóźwiak', 'F'), ('Leśniak', 'F'), ('Maj', 'F'), ('Śliwińska', 'F'), ('Urban', 'F'), ('Kowal', 'F'), ('Skiba', 'F'), ('Małecka', 'F'), ('Dobrowolska', 'F'), ('Bednarczyk', 'F'), ('Marek', 'F'), ('Socha', 'F'), ('Michalik', 'F'), ('Romanowska', 'F'), ('Wrona', 'F'), ('Domagała', 'F'), ('Kasprzak', 'F'), ('Wilczyńska', 'F'), ('Ratajczak', 'F'), ('Matuszewska', 'F'), ('Olejniczak', 'F'), ('Świątek', 'F'), ('Orzechowska', 'F'), ('Pająk', 'F'), ('Kurowska', 'F'), ('Bukowska', 'F'), ('Sobolewska', 'F'), ('Owczarek', 'F'), ('Grzelak', 'F'), ('Olejnik', 'F'), ('Mazurkiewicz', 'F'), ('Łukasik', 'F'), ('Rogowska', 'F'), ('Kędzierska', 'F'), ('Barańska', 'F'), ('Kosińska', 'F'), ('Matusiak', 'F'), ('Sobczyk', 'F'), ('Skowrońska', 'F'), ('Rybak', 'F'), ('Marcinkowska', 'F'), ('Marszałek', 'F'), ('Zych', 'F'), ('Bednarska', 'F'), ('Chrzanowska', 'F'), ('Sroka', 'F'), ('Bednarz', 'F'), ('Lisowska', 'F'), ('Świderska', 'F'), ('Kuczyńska', 'F'), ('Kozieł', 'F'), ('Morawska', 'F'), ('Kasprzyk', 'F'), ('Janiszewska', 'F'), ('Muszyńska', 'F'), ('Grzybowska', 'F'), ('Białek', 'F'), ('Chmiel', 'F'), ('Małek', 'F'), ('Jędrzejewska', 'F'), ('Kwiecień', 'F'), ('Witek', 'F'), ('Pluta', 'F'), ('Paluch', 'F'), ('Marczak', 'F'), ('Czaja', 'F'), ('Turek', 'F'), ('Czajka', 'F'), ('Osińska', 'F'), ('Krzemińska', 'F'), ('Piekarska', 'F'), ('Czech', 'F'), ('Kubicka', 'F'), ('Żukowska', 'F'), ('Janowska', 'F'), ('Michałowska', 'F'), ('Marzec', 'F'), ('Biernacka', 'F'), ('Gołębiewska', 'F'), ('Przybysz', 'F'), ('Szydłowska', 'F'), ('Staniszewska', 'F'), ('Śliwa', 'F'), ('Stefaniak', 'F'), ('Kaczor', 'F'), ('Serafin', 'F'), ('Kulesza', 'F'), ('Lech', 'F'), ('Murawska', 'F'), ('Rudnicka', 'F'), ('Smolińska', 'F'), ('Popławska', 'F'), ('Lewicka', 'F'), ('Podgórska', 'F'), ('Kujawa', 'F'), ('Dębska', 'F'), ('Cieślik', 'F'), ('Stachowiak', 'F'), ('Góra', 'F'), ('Kacprzak', 'F'), ('Pietrzyk', 'F'), ('Piątkowska', 'F'), ('Banach', 'F'), ('Niemiec', 'F'), ('Sowińska', 'F'), ('Karczewska', 'F'), ('Gołębiowska', 'F'), ('Żurek', 'F'), ('Banaś', 'F'), ('Matysiak', 'F'), ('Rosińska', 'F'), ('Klimczak', 'F'), ('Bieniek', 'F'), ('Żurawska', 'F'), ('Godlewska', 'F'), ('Augustyniak', 'F'), ('Rak', 'F'), ('Zawada', 'F'), ('Czyż', 'F'), ('Komorowska', 'F'), ('Grochowska', 'F'), ('Skrzypczak', 'F'), ('Gruszka', 'F'), ('Górka', 'F'), ('Kuś', 'F'), ('Różańska', 'F'), ('Szczęsna', 'F'), ('Przybyła', 'F'), ('Winiarska', 'F'), ('Cybulska', 'F'), ('Krawiec', 'F'), ('Panek', 'F'), ('Drozd', 'F'), ('Ptak', 'F'), ('Kulik', 'F'), ('Leśniewska', 'F'), ('Konopka', 'F'), ('Krzyżanowska', 'F'), ('Mikołajczak', 'F'), ('Gil', 'F'), ('Cichoń', 'F'), ('Szczepanik', 'F'), ('Słowik', 'F'), ('Cichocka', 'F'), ('Zarzycka', 'F'), ('Siwek', 'F'), ('Mielczarek', 'F'), ('Zaremba', 'F'), ('Graczyk', 'F'), ('Strzelecka', 'F'), ('Tokarska', 'F'), ('Szymczyk', 'F'), ('Młynarczyk', 'F'), ('Bartkowiak', 'F'), ('Kaczyńska', 'F'), ('Maćkowiak', 'F'), ('Kłos', 'F'), ('Stańczyk', 'F'), ('Buczek', 'F'), ('Niewiadomska', 'F'), ('Janus', 'F'), ('Grzyb', 'F'), ('Biernat', 'F'), ('Szczygieł', 'F'), ('Szostak', 'F'), ('Kostrzewa', 'F'), ('Filipiak', 'F'), ('Skowron', 'F'), ('Węgrzyn', 'F'), ('Janiak', 'F'), ('Górna', 'F'), ('Jurek', 'F'), ('Kucharczyk', 'F'), ('Rakowska', 'F'), ('Kaleta', 'F'), ('Sienkiewicz', 'F'), ('Bogusz', 'F'), ('Gąsior', 'F'), ('Gawron', 'F'), ('Cieśla', 'F'), ('Kubik', 'F'), ('Książek', 'F'), ('Bagińska', 'F'), ('Antczak', 'F'), ('Banasiak', 'F'), ('Różycka', 'F'), ('Bartczak', 'F'), ('Żebrowska', 'F'), ('Gawlik', 'F'), ('Królikowska', 'F'), ('Trzcińska', 'F'), ('Kula', 'F'), ('Zaręba', 'F'), ('Pałka', 'F'), ('Hajduk', 'F'), ('Mikulska', 'F'), ('Rzepka', 'F'), ('Rogalska', 'F'), ('Grzegorczyk', 'F'), ('Frączek', 'F'), ('Długosz', 'F'), ('Witczak', 'F'), ('Malec', 'F'), ('Maliszewska', 'F'), ('Rogala', 'F'), ('Rojek', 'F'), ('Tkaczyk', 'F'), ('Gawrońska', 'F'), ('Trojanowska', 'F'), ('Dobosz', 'F'), ('Radomska', 'F'), ('Rybicka', 'F'), ('Lach', 'F'), ('Borek', 'F'), ('Stec', 'F'), ('Mika', 'F'), ('Frankowska', 'F'), ('Kaczorowska', 'F'), ('Bogucka', 'F'), ('Kisiel', 'F'), ('Wąsik', 'F'), ('Ślusarczyk', 'F'), ('Więckowska', 'F'), ('Zaborowska', 'F'), ('Gałązka', 'F'), ('Grzesiak', 'F'), ('Kwaśniewska', 'F'), ('Kmiecik', 'F'), ('Karwowska', 'F'), ('Białas', 'F'), ('Frąckowiak', 'F'), ('Pietras', 'F'), ('Dudzińska', 'F'), ('Kubacka', 'F'), ('Juszczak', 'F'), ('Walkowiak', 'F'), ('Lipska', 'F'), ('Bochenek', 'F'), ('Sokół', 'F'), ('Janas', 'F'), ('Wnuk', 'F'), ('Nawrot', 'F'), ('Wojtczak', 'F'), ('Rosiak', 'F'), ('Wójcicka', 'F'), ('Lasota', 'F'), ('Kołodziejska', 'F'), ('Pawelec', 'F'), ('Mroczek', 'F'), ('Burzyńska', 'F'), ('Wrońska', 'F'), ('Zygmunt', 'F'), ('Czyżewska', 'F'), ('Skrzypek', 'F'), ('Szafrańska', 'F'), ('Fijałkowska', 'F'), ('Piórkowska', 'F'), ('Piwowarczyk', 'F'), ('Jędrzejczyk', 'F'), ('Łukasiewicz', 'F'), ('Zajączkowska', 'F'), ('Gruszczyńska', 'F'), ('Krysiak', 'F'), ('Borowiec', 'F'), ('Łapińska', 'F'), ('Masłowska', 'F'), ('Pasternak', 'F'), ('Jagodzińska', 'F'), ('Żmuda', 'F'), ('Dąbek', 'F'), ('Jurkiewicz', 'F'), ('Bujak', 'F'), ('Krakowiak', 'F'), ('Lisiecka', 'F'), ('Jędrzejczak', 'F'), ('Misiak', 'F'), ('Pilarska', 'F'), ('Jagiełło', 'F'), ('Sobieraj', 'F'), ('Twardowska', 'F'), ('Wilczek', 'F'), ('Gaweł', 'F'), ('Karaś', 'F'), ('Bielawska', 'F'), ('Gutowska', 'F'), ('Skibińska', 'F'), ('Łukaszewska', 'F'), ('Flis', 'F'), ('Kałuża', 'F'), ('Drzewiecka', 'F'), ('Strzelczyk', 'F'), ('Kubica', 'F'), ('Dębowska', 'F'), ('Stelmach', 'F'), ('Cygan', 'F'), ('Zielonka', 'F'), ('Sołtys', 'F'), ('Tarnowska', 'F'), ('Cebula', 'F'), ('Skoczylas', 'F'), ('Szcześniak', 'F'), ('Falkowska', 'F'), ('Raczyńska', 'F'), ('Bober', 'F'), ('Bilska', 'F'), ('Bartosik', 'F'), ('Filipek', 'F'), ('Pakuła', 'F'), ('Wasiak', 'F'), ('Jakubiak', 'F'), ('Stanek', 'F'), ('Gąsiorowska', 'F'), ('Mierzejewska', 'F'), ('Kędziora', 'F'), ('Majcher', 'F'), ('Szwed', 'F'), ('Górniak', 'F'), ('Krupińska', 'F'), ('Nowaczyk', 'F'), ('Guzik', 'F'), ('Staszewska', 'F'), ('Więcek', 'F'), ('Ciszewska', 'F'), ('Stolarczyk', 'F'), ('Drozdowska', 'F'), ('Rusin', 'F'), ('Czapla', 'F'), ('Urbańczyk', 'F'), ('Kopczyńska', 'F'), ('Jaśkiewicz', 'F'), ('Góral', 'F'), ('Wolak', 'F'), ('Matuszak', 'F'), ('Kulig', 'F'), ('Kaczmarska', 'F'), ('Gołąb', 'F'), ('Słowińska', 'F'), ('Cholewa', 'F'), ('Rucińska', 'F'), ('Milczarek', 'F');
LOCK TABLES `Names` READ, `Surnames` READ, `Children` WRITE;

INSERT INTO Children (Name,Surname,GroupId) SELECT Names.val, Surnames.val, 1 FROM Names LEFT JOIN Surnames ON Surnames.gender=Names.gender AND RAND()>0.97 WHERE RAND()>0.3;

UNLOCK TABLES;
