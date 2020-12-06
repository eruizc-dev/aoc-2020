#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int occurences(char ch, const char* pwd);
int is_valid_p1(int min, int max, char ch, const char* pwd);
int is_valid_p2(int pos1, int pos2, char ch, const char* pwd);

int main()
{
    int num1, num2, valid1 = 0, valid2 = 0;
    char character, password[256];
    FILE* file;

    file = fopen("./input.txt", "rt");
    if (!file)
    {
        fprintf(stderr, "Failed to open file\n");
        return EXIT_FAILURE;
    }

    while (fscanf(file, "%d-%d %c: %s\n", &num1, &num2, &character, password) != EOF)
    {
        if (is_valid_p1(num1, num2, character, password))
            valid1++;
        if (is_valid_p2(num1, num2, character, password))
            valid2++;
    }
    fclose(file);

    printf("Part 1 valid passwords: %d\n", valid1);
    printf("Part 2 valid passwords: %d\n", valid2);

    return EXIT_SUCCESS;
}

int is_valid_p1(int min, int max, char ch, const char* pwd)
{
    int q = occurences(ch, pwd);
    return min <= q && q <= max;
}

int occurences(char ch, const char* pwd)
{
    int q = 0;
    char *s;

    while (*pwd)
    {
        if (*pwd == ch)
            q++;
        pwd++;
    }

    return q;
}

int is_valid_p2(int pos1, int pos2, char ch, const char* pwd)
{
    int valid = 0;
    int len;

    len = strlen(pwd);

    if (pos1 <= len && ch == pwd[pos1 - 1])
        valid ^= 1;

    if (pos2 <= len && ch == pwd[pos2 - 1])
        valid ^= 1;

    return valid;
}
