CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE music (
	record_id uuid default uuid_generate_v4() PRIMARY KEY,
	artist varchar(100) not null,
	track varchar(100) not null,
	playlist varchar(50) not null,
	album varchar(100)
);
