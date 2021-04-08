INSERT INTO public.do_order (cargo_owner_id, do_order_number, created, creator)
VALUES (1, 'DO1', CURRENT_TIMESTAMP, 'test_set');
INSERT INTO do_order (cargo_owner_id, do_order_number, created, creator)
VALUES (1, 'DO2', CURRENT_TIMESTAMP, 'test_set');
INSERT INTO do_order (cargo_owner_id, do_order_number, created, creator)
VALUES (2, 'DO3', CURRENT_TIMESTAMP, 'test_set');
