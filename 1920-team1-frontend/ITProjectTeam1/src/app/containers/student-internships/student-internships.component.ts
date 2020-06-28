import {Component, OnInit} from '@angular/core';
import {Internship} from '../../models/internship';
import {InternshipService} from '../../services/internship.service';
import {FormControl} from '@angular/forms';

@Component({
    selector: 'app-student-internships',
    templateUrl: './student-internships.component.html',
    styleUrls: ['./student-internships.component.scss']
})
export class StudentInternshipsComponent implements OnInit {
    internships: Internship[];
    internship: Internship;
    copyInternships: Internship[];
    favoriteInternships: Internship[];
    courses = new FormControl();
    technologies = new FormControl();
    coursesList: any[] = [
        {name: 'TI - Applicatie-ontwikkeling', id: 1},
        {name: 'TI - Systemen en netwerkbeheer', id: 2},
        {name: 'TI - Software-management', id: 3},
        {name: 'Elektronica-ICT', id: 4},
    ];
    technologiesList: any [] = [
        {name: 'JAVA', id: 1},
        {name: '.NET', id: 2},
        {name: 'HTML', id: 3},
        {name: 'CSS', id: 4},
        {name: 'JavaScript', id: 5},
        {name: 'Angular', id: 6},
        {name: 'React', id: 7},
        {name: 'C++', id: 8},
        {name: 'C#', id: 9}
    ];
    filterInternships: Internship[] = [];

    constructor(private is: InternshipService) {
    }

    ngOnInit(): void {
        this.loadData();

        // FILTER COURSES
        this.courses.valueChanges.subscribe(result => {
            this.filterInternships.length = 0;
            const otherInternships = [...this.copyInternships];
            if (result.length) {
                result.forEach(course => {
                    // tslint:disable-next-line:max-line-length
                   const foundInternships = otherInternships.filter(internship => internship.courses.filter(item => item.name === course).length !== 0);
                   foundInternships.forEach(val => this.filterInternships.push(val));
                });
                this.internships = this.filterInternships;
            } else {
                this.internships = otherInternships;
            }
        });

        // FILTER TECHNOLOGIES
        this.technologies.valueChanges.subscribe(result => {
            this.filterInternships.length = 0;
            const otherInternships = [...this.copyInternships];
            if (result.length) {
                result.forEach(technology => {
                    // tslint:disable-next-line:max-line-length
                    const foundInternships = otherInternships.filter(internship => internship.technologies.filter(item => item.name === technology).length !== 0);
                    foundInternships.forEach(val => this.filterInternships.push(val));
                });
                this.internships = this.filterInternships;
            } else {
                this.internships = otherInternships;
            }
        });

    }


    loadData(): void {
        this.is.getStudentFavorites().subscribe((result: Internship[]) => {
            this.favoriteInternships = result;
            for (const item of result) {
                item.favorite = true;
            }
        });

        this.is.getInternshipsForStudents().subscribe((result: Internship[]) => {
            this.internships = result;
            this.copyInternships = [...this.internships];
            for (const item of result) {
                for (const fav of this.favoriteInternships) {
                    if (item.id === fav.id) {
                        item.favorite = true;
                    }
                }
            }
        });
    }

    showItem(event): void {
        this.is.getById(event).subscribe((result: Internship) => {
            this.internship = result;
        });
    }
}
