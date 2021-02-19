------------------------------------------------------------------------------------------------------------------------
-- SCHEMA
------------------------------------------------------------------------------------------------------------------------

-- public.history definition

-- Drop table

-- DROP TABLE public.history;

CREATE TABLE public.history
(
    id int NOT NULL GENERATED ALWAYS AS IDENTITY,
    rowstatus smallint NOT NULL DEFAULT 0,
    do_order_number varchar(24) NOT NULL,
    current_state varchar(24) NOT NULL,
    event_store varchar(1024) NOT NULL,
    created timestamp NOT NULL,
    creator varchar(64) NOT NULL,
    modified timestamp NULL,
    modifier varchar(64),
    CONSTRAINT history_id_pk PRIMARY KEY (id)
)
CREATE UNIQUE INDEX do_order_history_key ON public.history USING btree (do_order_number) WHERE rowstatus = 0;