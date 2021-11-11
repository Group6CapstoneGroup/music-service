CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE music (
	record_id uuid default uuid_generate_v4() PRIMARY KEY,
	record_number bigint not null,
	artist varchar(100) not null,
	track varchar(100) not null,
	playlist varchar(100) not null,
	album varchar(100) not null
);