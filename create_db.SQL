
create table grouptable
(
  group_id int not null auto_increment,
  group_name varchar(150) not null,
  max_age float,
  min_age float,
  group_size tinyint not null,
  group_coast float not null,
 primary key (group_id)
)
;


create table baby
(
  baby_id int not null auto_increment,
  baby_surname varchar(200) not null,
  baby_name varchar(200) not null,
  baby_patronomic varchar(200),
  health_certificat bool not null,
  baby_birthday date not null,
  group_id int,
 primary key (baby_id)
)
;

create index ix_relationship7 on baby (group_id)
;

create table immunization
(
  immunizations_id int not null auto_increment,
  dtp date,
  parotits date,
  tuberculosis date,
  poli date,
  baby_id int not null,
 primary key (immunizations_id,baby_id)
)
;

-- table parent

create table parent
(
  parent_id int not null auto_increment,
  parent_surname varchar(200) not null,
  parent_name varchar(200) not null,
  parent_patronomic varchar(200),
  parent_location varchar(350) not null,
  parent_phone varchar(15),
  parent_work varchar(350),
  parent_workphone varchar(15),
  baby_id int not null,
  parent_info varchar(100) not null,
 primary key (parent_id,baby_id)
)
;

-- table trustee

create table trustee
(
  trustee_id int not null auto_increment,
  trustee_surname varchar(200) not null,
  trustee_name varchar(200) not null,
  trustee_patronomic varchar(200),
  trustee_phone varchar(15),
  trustee_caption varchar(150) not null,
  baby_id int not null,
 primary key (trustee_id,baby_id)
)
;

-- table educator

create table educator
(
  educator_id int not null auto_increment,
  educator_surname varchar(200) not null,
  educator_name varchar(200) not null,
  educator_patronomic varchar(200),
  educator_phone varchar(15),
  educator_location varchar(350),
  group_id int,
 primary key (educator_id)
)
;

create index ix_relationship8 on educator (group_id)
;

-- table dayweek

create table dayweek
(
  dayweek_id int not null auto_increment,
  dayweek_name varchar(50) not null,
 primary key (dayweek_id)
)
;

-- table pay

create table pay
(
  pay_id int not null auto_increment,
  pay_balance double not null,
  baby_id int not null,
 primary key (pay_id,baby_id)
)
;

-- table shedulevisit

create table shedulevisit
(
  shedulevisit_id int not null auto_increment,
  baby_id int not null,
  date_begin datetime not null,
  date_end date,
 primary key (shedulevisit_id,baby_id)
)
;

-- table visiting

create table visiting
(
  visiting_id int not null auto_increment,
  visiting_date date not null,
  visiting_timebegin datetime not null,
  visiting_timeend datetime ,
  visiting_cost float not null,
  educator_id int,
  baby_id int not null,
  trustee_id int,
 primary key (visiting_id,baby_id)
)
;

create index ix_relationship36 on visiting (educator_id)
;

-- table eating

create table eating
(
  eating_id int not null auto_increment,
  coast int not null,
  eating_date date not null,
  nutrition_type_id int,
  baby_id int not null,
 primary key (eating_id,baby_id)
)
;

create index ix_relationship42 on eating (nutrition_type_id)
;

-- table string_shedulevisit

create table string_shedulevisit
(
  string_id int not null auto_increment,
  eat_breakfast bool not null,
  eat_snack bool not null,
  eat_lunch bool not null,
  eat_dinner bool not null,
  time_begin time not null,
  time_end time not null,
  dayweek_id int,
  shedulevisit_id int not null,
  baby_id int not null,
 primary key (string_id,shedulevisit_id,baby_id)
)
;

create index ix_relationship37 on string_shedulevisit (dayweek_id)
;

-- table nutrition_type

create table nutrition_type
(
  nutrition_type_id int not null auto_increment,
  nutrition_type_name varchar(200) not null,
 primary key (nutrition_type_id)
)
;

-- table eatshedule

create table eatshedule
(
  eatshedule_id int not null auto_increment,
  eatshedule_time_begin datetime not null,
  eatshedule_time_end datetime not null,
  eating_cost double not null,
  eatschedule_date_begin date not null,
  eatschedule_date_end date,
  nutrition_type_id int,
 primary key (eatshedule_id)
)
;

create index ix_relationship44 on eatshedule (nutrition_type_id)
;

-- create relationships section ------------------------------------------------- 

alter table baby add constraint relationship6 foreign key (group_id) references grouptable (group_id) on delete no action on update no action
;

alter table baby add constraint relationship7 foreign key (group_id) references grouptable (group_id) on delete restrict on update restrict
;

alter table educator add constraint relationship8 foreign key (group_id) references grouptable (group_id) on delete restrict on update restrict
;

alter table visiting add constraint relationship36 foreign key (educator_id) references educator (educator_id) on delete restrict on update restrict
;

alter table string_shedulevisit add constraint relationship37 foreign key (dayweek_id) references dayweek (dayweek_id) on delete restrict on update restrict
;

alter table string_shedulevisit add constraint relationship40 foreign key (shedulevisit_id, baby_id) references shedulevisit (shedulevisit_id, baby_id) on delete cascade on update restrict
;

alter table eating add constraint relationship42 foreign key (nutrition_type_id) references nutrition_type (nutrition_type_id) on delete restrict on update restrict
;

alter table eatshedule add constraint relationship44 foreign key (nutrition_type_id) references nutrition_type (nutrition_type_id) on delete restrict on update restrict
;

alter table pay add constraint relationship45 foreign key (baby_id) references baby (baby_id) on delete cascade on update restrict
;

alter table eating add constraint relationship46 foreign key (baby_id) references baby (baby_id) on delete cascade on update restrict
;

alter table shedulevisit add constraint relationship47 foreign key (baby_id) references baby (baby_id) on delete cascade on update restrict
;

alter table immunization add constraint relationship48 foreign key (baby_id) references baby (baby_id) on delete cascade on update restrict
;

alter table parent add constraint relationship49 foreign key (baby_id) references baby (baby_id) on delete cascade on update restrict
;

alter table trustee add constraint relationship50 foreign key (baby_id) references baby (baby_id) on delete cascade on update restrict
;

alter table visiting add constraint relationship52 foreign key (baby_id) references baby (baby_id) on delete cascade on update restrict
;

insert into nutrition_type (nutrition_type_name) values('Завтрак');
insert into nutrition_type (nutrition_type_name) values('Обед');
insert into nutrition_type (nutrition_type_name) values('Полдник');
insert into nutrition_type (nutrition_type_name) values('Ужин');

insert into dayweek (dayweek_name) values('Понедельник');
insert into dayweek (dayweek_name) values('Вторник');
insert into dayweek (dayweek_name) values('Среда');
insert into dayweek (dayweek_name) values('Четверг');
insert into dayweek (dayweek_name) values('Пятница');
insert into dayweek (dayweek_name) values('Суббота');
insert into dayweek (dayweek_name) values('Воскресенье');





