program main
    implicit none

    integer :: i, j, charval, g, p1_total, p2_total, p1_group_total(1024), p2_group_total(27, 1024), gcount
    logical :: p1_seen(26)
    character(len=26) :: line

    ! Set initial
    g = 1
    gcount = 0
    p1_total = 0
    p2_total = 0
    do i=1,1024
        p1_group_total(i) = 0
        do j=1,27
            p2_group_total(j, i) = 0
        end do
    end do
    do i=1,26
        p1_seen(j) = .false.
    end do

    ! Read file
    open(1, file='input.txt', status='old')
        do i=1,2242
            read (1, '(A26)') line

            if (line .eq. '') then
                ! If new line, move next group and reset
                g = g + 1
                gcount = 0
                do j=1,26
                    p1_seen(j) = .false.
                end do
            else
                ! Check for new chars
                p2_group_total(27, g) = p2_group_total(27, g) + 1
                do j=1,26
                    charval = iachar(line(j:j)) - iachar('a') + 1
                    if (line(j:j) .eq. '') then
                        continue
                    else if (p1_seen(charval) .eqv. .false.) then
                        p1_group_total(g) = p1_group_total(g) + 1
                        p1_seen(charval) = .true.
                    endif

                    if (line(j:j) .eq. '') then
                        continue
                    else
                        p2_group_total(charval, g) = p2_group_total(charval, g) + 1
                    endif
                end do
            endif
        end do
    close(1)

    ! Sum p1_group_total
    do g=1,1024
        p1_total = p1_total + p1_group_total(g)
        do i=1, 26
            if (p2_group_total(27, g) /= 0 .and. p2_group_total(i, g) == p2_group_total(27, g)) then
                p2_total = p2_total + 1
            endif 
        end do
    end do

    print *, "Part 1: ", p1_total
    print *, "Part 2: ", p2_total

end program main
